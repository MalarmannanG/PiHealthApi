using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using PiHealth.DataModel.Entity;
using PiHealth.Services.UserAccounts;
using Microsoft.Extensions.DependencyInjection;
using PiHealth.Service.UserAccounts;

namespace PiHealth.Controllers
{
    [Produces("application/json")]
    public class BaseApiController : ControllerBase
    {      
      
        public AppUser ActiveUser
        {
            get
            {
                var userService = this.HttpContext.RequestServices.GetRequiredService<IAppUserService>();
                return userService.ActiveUser;
            }
        }

        public string ControllerName
        {
            get
            {
                return ControllerContext.ActionDescriptor.ControllerName;
            }
        }

        public string ActionName
        {
            get
            {
                return ControllerContext.ActionDescriptor.ActionName;
            }
        }

        public string RequestIP
        {
            get
            {
                return ControllerContext.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }

        public string UserAgent
        {
            get
            {
                return ControllerContext.HttpContext.Request.Headers["User-Agent"];
            }
        }

    }
}