using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PiHealth.Service.UserAccounts;
using PiHealth.Services.UserAccounts;
using PiHealth.Web.Model;
using PiHealth.Controllers;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PiHealth.Web.Model.Account;
using PiHealth.Services;
using PiHealth.Services.AccessServices;
using PiHealth.Web.Model.AccessRights;
using Microsoft.EntityFrameworkCore;
using PiHealth.DataModel.Options;
using PiHealth.Web.Helper;
using PiHealth.Web.Filter;

namespace PiHealth.Web.Controllers
{

    [Route("Api/[Controller]")]
    [Produces("application/json")]
    public class AccountController : BaseApiController
    {
        private readonly IAppUserService _usersService;
        private readonly ITokenStoreService _tokenStoreService;
        private readonly SecurityService _securityService;
        private readonly AuditLogServices _auditLogService;
        private readonly AccessFunctionService _accessFunctionService;
        private readonly AccessModuleService _accessModuleService;
        private readonly AccessRoleFunctionService _accessRoleService;

        public AccountController(
            AccessFunctionService accessFunctionService,
            AccessModuleService accessModuleService,
            AccessRoleFunctionService accessRoleService,
            IAppUserService usersService,
            ITokenStoreService tokenStoreService,
            SecurityService securityService,
            AuditLogServices auditLogServices)

        {
            _accessFunctionService = accessFunctionService;
            _accessModuleService = accessModuleService;
            _accessRoleService = accessRoleService;
            _usersService = usersService;
            _usersService.CheckArgumentIsNull(nameof(usersService));
            _securityService = securityService;
            _auditLogService = auditLogServices;
            _tokenStoreService = tokenStoreService;
            _tokenStoreService.CheckArgumentIsNull(nameof(_tokenStoreService));
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        [CustomExceptionFilter]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginUser)
        {

            if (loginUser == null)
            {
                return BadRequest();
            }

            var user = await _usersService.FindUserAsync(loginUser.Email.Trim(), loginUser.Password).ConfigureAwait(false);

            if (!user.IsActive)
            {
                _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, userid: user.Id, RequestIP: RequestIP, value1: $"Email-{loginUser.Email} Is Inactive", value2: "Failed");
                return BadRequest();
            }

            if (user == null)
            {
                _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, userid: user.Id, RequestIP: RequestIP, value1: $"Email-{loginUser.Email}", value2: "Failed");
                return BadRequest();
            }

            var (accessToken, refreshToken) = await _tokenStoreService.CreateJwtTokens(user).ConfigureAwait(false);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, userid: user.Id, RequestIP: RequestIP, value1: $"Email-{loginUser.Email}", value2: "Success");

            return Ok(new { token = accessToken, role = user.UserType });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody]JToken jsonBody)
        {
            var refreshToken = jsonBody.Value<string>("refreshToken");
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return BadRequest("refreshToken is not set.");
            }

            var token = await _tokenStoreService.FindTokenAsync(refreshToken);
            if (token == null)
            {
                return Unauthorized();
            }

            var (accessToken, newRefreshToken) = await _tokenStoreService.CreateJwtTokens(token.AppUser).ConfigureAwait(false);
            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, userid: ActiveUser.Id, RequestIP: RequestIP, value1: "Success", value2: "Token Refreshed for the user " + token.AppUser.Name + ". New Token: " + refreshToken);
            return Ok(new { access_token = accessToken, refresh_token = newRefreshToken, username = token.AppUser.Name });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAccess/{roleName}")]
        public async Task<AccessRightsModel> GetAccess(string roleName)
        {

            var moduleFunctions = await _accessFunctionService.GetAll().Include(a => a.AccessModule).AsQueryable().ToListAsync();

            var data = new AccessRightsModel();

            data.roles = typeof(UserType).ToSelectListItems().Select(a => new RoleDetails()
            {
                name = a.Text,
                value = a.Value

            }).ToList();

            data.role = string.IsNullOrEmpty(roleName) ? UserType.Admin.ToString() : roleName;

            var functions = _accessRoleService.GetAll(role: roleName).Select(a => a.FunctionID).ToList();

            foreach (var each in moduleFunctions.GroupBy(a => a.ModuleID))
            {
                var menus = new ModuleDetails();

                menus.code = each.FirstOrDefault().AccessModule.ModuleCode;

                menus.id = each.Key;

                menus.name = each.FirstOrDefault().AccessModule.Name;

                menus.functions = each.Select(a => new FunctionDetails()
                {
                    id = a.Id,
                    code = a.FuctionCode,
                    haveAccess = functions.Contains(a.Id),
                    name = a.Description
                }).ToList();

                data.modules.Add(menus);
            }

            return data;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("Logout")]
        public async Task<bool> Logout()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;

            var userIdValue = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // The Jwt implementation does not support "revoke OAuth token" (logout) by design.
            // Delete the user's tokens from the database (revoke its bearer token)
            if (!string.IsNullOrWhiteSpace(userIdValue) && int.TryParse(userIdValue, out int userId))
            {
                await _tokenStoreService.InvalidateUserTokensAsync(userId).ConfigureAwait(false);
            }

            await _tokenStoreService.DeleteExpiredTokensAsync().ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("IsAuthenthenticated")]
        public bool IsAuthenthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpGet]
        [Route("GetUserInfo")]
        public IActionResult GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return Ok(new { Username = claimsIdentity.Name });
        }

        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordModel changePassword)
        {
            if (changePassword == null)
                return BadRequest();

            var oUser = ActiveUser;
            var hashed = _securityService.GetSha256Hash(changePassword.oldPassword);
            var oldPassworWrong = hashed != oUser.Password;

            if (oldPassworWrong)
            {
                return Ok(new { status = false });
            }

            oUser.Password = _securityService.GetSha256Hash(changePassword.newPassword);

            _usersService.Update(oUser);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, userid: ActiveUser.Id, RequestIP: RequestIP, value1: "Success", value2: "Password changed from " + changePassword.oldPassword + " to " + changePassword.newPassword);

            return Ok(new { status = true });

        }
    }
}
