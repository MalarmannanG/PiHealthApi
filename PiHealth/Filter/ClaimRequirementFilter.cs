using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using PiHealth.Service.UserAccounts;
using PiHealth.Services.AccessServices;

namespace PiHealth.Web.Filters
{
    public class ClaimRequirementFilter : Attribute, IAuthorizationFilter
    {
        public string FunctionCode { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasClaim = false;

            var accessDetails = context.HttpContext.RequestServices.GetRequiredService<AccessRoleFunctionService>();

            var user = context.HttpContext.RequestServices.GetRequiredService<IAppUserService>().ActiveUser;

            if (user == null)
                context.Result = new ForbidResult();

            var data = accessDetails.GetAll(role: user.UserType);

            hasClaim = accessDetails.GetAll(role: user.UserType).Where(a => a.AccessFunctions.FuctionCode == FunctionCode).Any();

            if (!hasClaim)
                context.Result = new ForbidResult();
        }
    }
}
    