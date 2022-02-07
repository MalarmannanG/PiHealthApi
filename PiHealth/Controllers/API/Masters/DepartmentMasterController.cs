using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using PiHealth.Web.Filters;
using PiHealth.DataModel.Entity;
using PiHealth.Service.UserAccounts;
using PiHealth.Services;
using PiHealth.Services.Master;
using PiHealth.Web.MappingExtention;
using PiHealth.Web.Model.Department;
using PiHealth.Controllers;

namespace PiHealth.Web.Controllers.API.Masters
{
    [Authorize]
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class DepartmentMasterController : BaseApiController
    {
        private readonly DepartmentService _departmentService;
        private readonly IAppUserService _appUserService;        
        private readonly AuditLogServices _auditLogService;

        public DepartmentMasterController(
            DepartmentService departmentService,
            IAppUserService appUserService,            
            AuditLogServices auditLogServices)
        {
            _departmentService = departmentService;
            _appUserService = appUserService;            
            _auditLogService = auditLogServices;
        }

        #region  Department Master

        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult Get(long id)
        {
            var department = _departmentService.Get(id).ToModel(new DepartmentMasterModel());
            return Ok(department);
        }

        [HttpGet]
        [Route("GetAll")]
        [ClaimRequirementFilter(FunctionCode = "P5-C1")]
        public IActionResult GetAll()
        {
            var departments = _departmentService.GetActive().OrderByDescending(a => a.CreatedDate).Select(a => a.ToModel()).ToList();
            return Ok(departments);
        }

        [HttpPost]
        [Route("Create")]
        [ClaimRequirementFilter(FunctionCode = "P5-C2")]
        public IActionResult Create([FromBody] DepartmentMasterModel model)
        {
            var department = model.ToEntity(new DepartmentMaster());
            department.CreatedDate = DateTime.Now;
            department.CreatedBy = ActiveUser.Id;
            _departmentService.Add(department);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(department);
        }

        [HttpPost]
        [Route("Update")]
        [ClaimRequirementFilter(FunctionCode = "P5-C3")]
        public IActionResult Update([FromBody] DepartmentMasterModel model)
        {
            if (model == null)
                return BadRequest();

            var department = _departmentService.Get(model.id.Value);

            if (department == null)
                return BadRequest();

            department.Name = model.name;
            department.Description = model.description;
            department.IsActive = !model.isDeleted;
            department.UpdatedDate = DateTime.Now;
            department.UpdatedBy = ActiveUser.Id;

            _departmentService.Update(department);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(model);

        }

        [HttpPost]
        [Route("Delete")]
        [ClaimRequirementFilter(FunctionCode = "P5-C4")]
        public IActionResult Delete([FromBody] DepartmentMasterModel model)
        {
            if (model?.id == null)
                return BadRequest();

            var department = _departmentService.Get(model.id.Value);

            if (department == null)
                return BadRequest();

            department.IsActive = false;

            department.UpdatedDate = DateTime.Now;
            department.UpdatedBy = ActiveUser.Id;

            _departmentService.Update(department);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(model);
        }

        #endregion Department Ends
    }
}