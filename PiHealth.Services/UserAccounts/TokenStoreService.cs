using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PiHealth.Services;
using PiHealth.DataModel.Entity;
using PiHealth.Services.UserAccounts;
using PiHealth.Services.AccessServices;

namespace PiHealth.Service.UserAccounts
{
    public interface ITokenStoreService
    {
        Task AddUserTokenAsync(UserToken userToken);
        Task AddUserTokenAsync(
                AppUser user, string refreshToken, string accessToken,
                DateTimeOffset refreshTokenExpiresDateTime, DateTimeOffset accessTokenExpiresDateTime);
        Task<bool> IsValidTokenAsync(string accessToken, long userId);
        Task DeleteExpiredTokensAsync();
        Task<UserToken> FindTokenAsync(string refreshToken);
        Task DeleteTokenAsync(string refreshToken);
        Task InvalidateUserTokensAsync(long userId);
        Task<(string accessToken, string refreshToken)> CreateJwtTokens(AppUser user);
    }

    public class TokenStoreService : ITokenStoreService
    {      

        private readonly ITokenService _tokenService;
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;
        private readonly SecurityService _securityService;
        private readonly IAppUserService _appUserService;
        private readonly AccessRoleFunctionService _accessRoleFunctionService;

        public TokenStoreService(      
            SecurityService securityService,      
            IOptionsSnapshot<BearerTokensOptions> configuration,
            ITokenService tokenService,
            IAppUserService appUserService,
            AccessRoleFunctionService accessRoleFunctionService)
        {       

            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));
            _tokenService = tokenService;
             _configuration = configuration;
            _configuration.CheckArgumentIsNull(nameof(configuration));
            _appUserService = appUserService;
            _accessRoleFunctionService = accessRoleFunctionService;
        }

        public async Task AddUserTokenAsync(UserToken userToken)
        {
            await InvalidateUserTokensAsync(userToken.UserId).ConfigureAwait(false);
            _tokenService.Add(userToken);
        }

        public async Task AddUserTokenAsync(
                AppUser user, string refreshToken, string accessToken,
                DateTimeOffset refreshTokenExpiresDateTime, DateTimeOffset accessTokenExpiresDateTime)
        {

            var token = new UserToken
            {
                UserId = user.Id,
                // Refresh token handles should be treated as secrets and should be stored hashed
                RefreshTokenIdHash = _securityService.GetSha256Hash(refreshToken),
                AccessTokenHash = _securityService.GetSha256Hash(accessToken),
                RefreshTokenExpiresDateTime = refreshTokenExpiresDateTime,
                AccessTokenExpiresDateTime = accessTokenExpiresDateTime
            };

            await AddUserTokenAsync(token).ConfigureAwait(false);
        }

        public async Task DeleteExpiredTokensAsync()
        {
            var now = DateTimeOffset.UtcNow;

            var expiredTokens = await _tokenService.GetAll().Where(x => x.AccessTokenExpiresDateTime < now).ToListAsync();

            foreach (var token in expiredTokens)
            {
                _tokenService.Delete(token);
            }
        }

        public async Task DeleteTokenAsync(string refreshToken)
        {
            var token = await FindTokenAsync(refreshToken).ConfigureAwait(false);

            if (token != null)
            {
                _tokenService.Delete(token);
            }
        }

        public Task<UserToken> FindTokenAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return null;
            }
            var refreshTokenIdHash = _securityService.GetSha256Hash(refreshToken);
            return _tokenService.GetAll().FirstOrDefaultAsync(x => x.RefreshTokenIdHash == refreshTokenIdHash);
        }

        public async Task InvalidateUserTokensAsync(long userId)
        {
            var userTokens = await _tokenService.GetAll().Where(x => x.UserId == userId).ToListAsync().ConfigureAwait(false);

            foreach (var userToken in userTokens)
            {
                _tokenService.Delete(userToken);
            }
        }

        public async Task<bool> IsValidTokenAsync(string accessToken, long userId)
        {
            var accessTokenHash = _securityService.GetSha256Hash(accessToken);
            var userToken = await _tokenService.GetAll().FirstOrDefaultAsync(
                x => x.AccessTokenHash == accessTokenHash && x.UserId == userId).ConfigureAwait(false);
            return userToken?.AccessTokenExpiresDateTime >= DateTime.UtcNow;
        }

        public async Task<(string accessToken, string refreshToken)> CreateJwtTokens(AppUser user)
        {
            var now = DateTimeOffset.UtcNow;
            var accessTokenExpiresDateTime = now.AddMinutes(_configuration.Value.AccessTokenExpirationMinutes);
            var refreshTokenExpiresDateTime = now.AddMinutes(_configuration.Value.RefreshTokenExpirationMinutes);
            var accessToken = await createAccessTokenAsync(user, accessTokenExpiresDateTime.UtcDateTime);
            var refreshToken = Guid.NewGuid().ToString().Replace("-", "");

            await AddUserTokenAsync(user, refreshToken, accessToken, refreshTokenExpiresDateTime, accessTokenExpiresDateTime).ConfigureAwait(false);
            await _appUserService.Update(user).ConfigureAwait(false);

            return (accessToken, refreshToken);
        }

        private async Task<string> createAccessTokenAsync(AppUser user, DateTime expires)
        {
            return await Task.Run(() =>
            {
                var claims = new List<Claim>
            {
                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _configuration.Value.Issuer),
                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),                               
                new Claim(ClaimTypes.Role, user.UserType),
                // to invalidate the cookie
                new Claim(ClaimTypes.SerialNumber, user.SerialNumber),
                // custom data
                new Claim("id", user.Id.ToString()),
                new Claim("userName", user.Username),
                new Claim("userType", user.UserType),
            }; 

                var access =  _accessRoleFunctionService.GetAll(role: user.UserType)?.Select(a => a.AccessFunctions.FuctionCode)?.ToList();
                
                if (access?.Count > 0)
                {
                    var rights = string.Join(",", access);
                    claims.Add(new Claim("AccessRights", rights));
                }
                   


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration.Value.Issuer,
                    audience: _configuration.Value.Audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: expires,
                    signingCredentials: creds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }
    }
}