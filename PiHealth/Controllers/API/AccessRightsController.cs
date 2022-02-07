using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PiHealth.Services.AccessServices;
using Microsoft.EntityFrameworkCore;
using PiHealth.Web.Model.AccessRights;
using PiHealth.DataModel.Options;
using PiHealth.Web.Helper;
using PiHealth.Controllers;
using PiHealth.DataModel.Entity;
using Microsoft.AspNetCore.Authorization;
using PiHealth.Services;
using PiHealth.Web.Filters;
using PiHealth.Services.UserAccounts;

namespace PiHealth.Web.Controllers.API
{
    [Authorize]
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class AccessRightsController : BaseApiController
    {
        private readonly AccessFunctionService _accessFunctionService;
        private readonly AccessModuleService _accessModuleService;
        private readonly AccessRoleFunctionService _accessRoleService;
        private readonly AuditLogServices _auditLogService;

        public AccessRightsController(
            AccessFunctionService accessFunctionService,
            AccessModuleService accessModuleService,
            AccessRoleFunctionService accessRoleService,
            AuditLogServices auditLogServices)
        {
            _accessFunctionService = accessFunctionService;
            _accessModuleService = accessModuleService;
            _accessRoleService = accessRoleService;
            _auditLogService = auditLogServices;
        }

        [HttpGet]        
        [Route("GetAll/{roleName}")]
        [ClaimRequirementFilter(FunctionCode = "P13-C1")]
        public async Task<IActionResult> GetAll(string roleName = null)
        {

            var moduleFunctions = await _accessFunctionService.GetAll().Include(a => a.AccessModule).AsQueryable().ToListAsync();

            var data = new AccessRightsModel();
            data.roles = typeof(UserType).ToSelectListItems().Select(a => new RoleDetails()
            {
                name = a.Text,
                value = a.Value

            }).ToList();

            data.role = string.IsNullOrEmpty(roleName) ? UserType.Admin.ToString() : roleName;
            var role = _accessRoleService.GetAll(role: roleName).Include(a => a.AccessFunctions);

            foreach (var each in moduleFunctions.GroupBy(a => a.ModuleID))
            {
                data.modules.Add(new ModuleDetails()
                {
                    code = each.FirstOrDefault().AccessModule.ModuleCode,
                    id = each.Key,
                    name = each.FirstOrDefault().AccessModule.Name,
                    functions = each.Select(a => new FunctionDetails()
                    {
                        id = a.Id,
                        code = a.FuctionCode,
                        haveAccess = role.Any(b => b.FunctionID == a.Id),
                        name = a.Name
                    }).ToList()
                });
            }

            return Ok(data);
        }

        [HttpPost("[action]")]
        [ClaimRequirementFilter(FunctionCode = "P13-C2")]
        public async Task<IActionResult> UpdateRoleFunction([FromBody] AccessRightsModel model)
        {
            if (model == null)
                return BadRequest();

            var sysRoleFuctions = _accessRoleService.GetAll(role: model.role).ToList();

            if (sysRoleFuctions?.Count() > 0)
            {
                foreach (var each in sysRoleFuctions)
                {
                    _accessRoleService.Delete(each);
                }
            }

            var functionIds = new List<long>();

           if(model.modules?.Count > 0)
            {
                foreach (var item in model.modules)
                {
                    var functions = item.functions?.Where(a => a.haveAccess)?.Select(a => a.id).ToList();
                    if (functions?.Count > 0)
                    {
                        sysRoleFuctions = functions?.Select(
                                            a => new AccessRoleFunction()
                                            {
                                                Role = model.role,
                                                FunctionID = a
                                            }).ToList();

                        await _accessRoleService.Create(sysRoleFuctions);
                    }
                }
            }

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, userid: ActiveUser.Id, RequestIP: RequestIP, value1: "Success", value2: "Access role updated for the role " + model.role);

            return Ok(model);
        }
    }
}