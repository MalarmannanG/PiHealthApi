using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PiHealth.DataModel;
using PiHealth.DataModel.Entity;
using PiHealth.Service.UserAccounts;
using PiHealth.Services.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace PiHealth.Services.DBContext
{
    public interface IDbInitializerService
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }


    public class DbInitializerService : IDbInitializerService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly SecurityService _securityService;
        private readonly IAppUserService _appUserService;

        public DbInitializerService(
            IServiceScopeFactory scopeFactory,
            SecurityService securityService,
             IAppUserService appUserService)
        {
            _scopeFactory = scopeFactory;
            _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));

            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));
            _appUserService = appUserService;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<PiHealthDBContext>())
                {
                  context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
              var userService = serviceScope.ServiceProvider.GetRequiredService<IAppUserService>();
              var security = serviceScope.ServiceProvider.GetRequiredService<SecurityService>();
                if (!userService.GetAll().Any())
                {
                    var user = new AppUser()
                    {
                        Address = "",
                        Email = "admin@pisystems.com",
                        Name = "Admin",
                        IsActive = true,
                        Password = security.GetSha256Hash("123456"),
                        SerialNumber = "1",
                        Username = "Admin",
                        Gender = "Male",
                        UserType = "Admin",
                        PhoneNo = ""                        
                    };

                    userService.Create(user);

                    var doctor = new AppUser()
                    {
                        Address = "",
                        Email = "doctor@pisystems.com",
                        Name = "Doctor",
                        IsActive = true,
                        Password = security.GetSha256Hash("123456"),
                        SerialNumber = "2",
                        Username = "Admin",
                        Gender = "Male",
                        UserType = "Doctor",
                        PhoneNo = ""
                    };

                    userService.Create(doctor);

                }

            }
        }

    }
}
