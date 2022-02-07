using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.Web.Filters;
using PiHealth.DataModel.Entity;
using PiHealth.Service.UserAccounts;
using PiHealth.Services;
using PiHealth.Services.Master;
using PiHealth.Web.MappingExtention;
using PiHealth.Web.Model.TemplateMaster;
using PiHealth.Controllers;

namespace PiHealth.Web.Controllers.API.Masters
{
    [Authorize]
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class TemplateMasterController : BaseApiController
    {
        private readonly TemplateMasterService _templateMasterService;
        private readonly IAppUserService _appUserService;
        private readonly AuditLogServices _auditLogService;

        public TemplateMasterController(
            TemplateMasterService templateMasterService,
            IAppUserService appUserService,
            AuditLogServices auditLogServices)
        {
            _templateMasterService = templateMasterService;
            _appUserService = appUserService;
            _auditLogService = auditLogServices;
        }

        #region  Template Master

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _templateMasterService.Get(id);
            var templateMaster = result.ToModel(new TemplateMasterModel());
            return Ok(templateMaster);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll([FromQuery] TemplateQueryModel model)
        {
            var entities = _templateMasterService.GetAll(name: model.name);
            var total = entities.Count();
            entities = entities.OrderByDescending(a => a.CreatedDate).Skip(model.skip);
            if (model.take > 0)
            {
                entities = entities.Take(model.take);
            }
            var result = entities.ToList()?.Select(a => a.ToModel(new TemplateMasterModel())).ToList();
            return Ok(new { result, total });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] TemplateMasterModel model)
        {
            var templateMaster = model.ToEntity(new TemplateMaster());
            templateMaster.CreatedDate = DateTime.Now;
            templateMaster.CreatedBy = ActiveUser.Id;
            await _templateMasterService.Create(templateMaster);
            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(templateMaster);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] TemplateMasterModel model)
        {
            if (model == null)
                return BadRequest();

            var templateMaster = await _templateMasterService.UpdateGet(model.id);

            if (templateMaster == null)
                return BadRequest();

            templateMaster = model.ToEntity(templateMaster);
            templateMaster.IsDeleted = model.isDeleted;
            templateMaster.ModifiedDate = DateTime.Now;
            templateMaster.ModifiedBy = ActiveUser.Id;
            await _templateMasterService.Update(templateMaster);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(model);

        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var templateMaster = await _templateMasterService.Get(id);

            if (templateMaster == null)
                return BadRequest();

            templateMaster.IsDeleted = true;
            templateMaster.ModifiedDate = DateTime.Now;
            templateMaster.ModifiedBy = ActiveUser.Id;

            await _templateMasterService.Update(templateMaster);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok();
        }

        #endregion Template Master Ends
    }
}