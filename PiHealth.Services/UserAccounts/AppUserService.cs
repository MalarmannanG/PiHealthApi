using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PiHealth.DataModel;
using PiHealth.DataModel.Entity;
using PiHealth.Services.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PiHealth.Service.UserAccounts
{
    public interface IAppUserService
    {
        IQueryable<AppUser> GetAll(string name = null);
        Task<AppUser> FindUserAsync(long userId);

        Task<AppUser> Update(AppUser user);
        Task<AppUser> Create(AppUser user);
        Task<AppUser> FindUserAsync(string email, string password);
        bool EmailAlreadyExit(string email, long? userId = null);
        Task UpdateUserLastActivityDateAsync(long userId);      

        AppUser GetByID(long id);

        AppUser ActiveUser { get; }

    }
    public class AppUserService : IAppUserService
    {
        public readonly IRepository<AppUser> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SecurityService _securityService;
        public AppUserService(IRepository<AppUser> repository,
            SecurityService securityService,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _securityService = securityService;
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual IQueryable<AppUser> GetAll(string name = null)   
        {
            var data = _repository.Table.Where(a => a.IsActive);

            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(a => a.Name != null && a.Name.Contains(name));
            }

            return data;
        }

        public virtual Task<AppUser> Update(AppUser user)
        {
            return _repository.UpdateAsync(user);
        }

        public virtual Task<AppUser> Create(AppUser user)
        {
            return _repository.InsertAsync(user);
        }


        public virtual Task<AppUser> FindUserAsync(long userId)
        {
            return  _repository.Table.FirstOrDefaultAsync(a=> a.Id == userId);
        }

        public Task<AppUser> FindUserAsync(string email, string password)
        {
            var passwordHash = _securityService.GetSha256Hash(password);
            return _repository.Table.FirstOrDefaultAsync(x => x.Email == email && x.Password == passwordHash);
        }

        public bool EmailAlreadyExit(string email, long? userId = null)
        {
            return GetAll().Any(x => x.Email == email && x.Id != userId);
        }

        public virtual async Task<AppUser> UpdateAsync(AppUser user)
        {
            return await _repository.UpdateAsync(user);
        }

        public async Task UpdateUserLastActivityDateAsync(long userId)
        {
            var user = await FindUserAsync(userId).ConfigureAwait(false);
            if (user.LastLoggedIn != null)
            {
                var updateLastActivityDate = TimeSpan.FromMinutes(2);
                var currentUtc = DateTimeOffset.UtcNow;
                var timeElapsed = currentUtc.Subtract(user.LastLoggedIn.Value);
                if (timeElapsed < updateLastActivityDate)
                {
                    return;
                }
            }
            user.LastLoggedIn = DateTimeOffset.UtcNow;
            await UpdateAsync(user);
        }

        public virtual AppUser GetByID(long id)
        {
            return _repository.Table.FirstOrDefault(a => a.Id == id);
        }

        public virtual AppUser ActiveUser
        {
            get
            {
                var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
                var userIdValue = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return GetByID(Int32.Parse(userIdValue));
            }
        }

    }
}
