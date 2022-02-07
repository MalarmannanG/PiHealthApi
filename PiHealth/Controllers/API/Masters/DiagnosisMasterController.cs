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
using PiHealth.Web.Model.DiagnosisMaster;
using PiHealth.Controllers;

namespace PiHealth.Web.Controllers.API.Masters
{
    [Authorize]
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class DiagnosisMasterController : BaseApiController
    {
        private readonly DiagnosisMasterService _diagnosisMasterService;
        private readonly IAppUserService _appUserService;
        private readonly AuditLogServices _auditLogService;

        public DiagnosisMasterController(
            DiagnosisMasterService diagnosisMasterService,
            IAppUserService appUserService,
            AuditLogServices auditLogServices)
        {
            _diagnosisMasterService = diagnosisMasterService;
            _appUserService = appUserService;
            _auditLogService = auditLogServices;
        }

        #region  Diagnosis Master

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _diagnosisMasterService.Get(id);
            var diagnosis = result.ToModel(new DiagnosisMasterModel());
            return Ok(diagnosis);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll([FromQuery] DiagnosisQueryModel model)
        {
            var entities = _diagnosisMasterService.GetAll(name: model.name);
            var total = entities.Count();
            entities = entities.OrderByDescending(a => a.CreatedDate).Skip(model.skip);
            if (model.take > 0)
            {
                entities = entities.Take(model.take);
            }
            var result = entities.ToList()?.Select(a => a.ToModel(new DiagnosisMasterModel())).ToList();
            return Ok(new { result, total });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] DiagnosisMasterModel model)
        {
            var diagnosis = model.ToEntity(new DiagnosisMaster());
            diagnosis.CreatedDate = DateTime.Now;
            diagnosis.CreatedBy = ActiveUser.Id;
            await _diagnosisMasterService.Create(diagnosis);
            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(diagnosis);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] DiagnosisMasterModel model)
        {
            if (model == null)
                return BadRequest();

            var diagnosis = await _diagnosisMasterService.Get(model.id.Value);

            if (diagnosis == null)
                return BadRequest();

            diagnosis.Name = model.name;
            diagnosis.Description = model.description;
            diagnosis.IsDeleted = model.isDeleted;
            diagnosis.ModifiedDate = DateTime.Now;
            diagnosis.ModifiedBy = ActiveUser.Id;
            await _diagnosisMasterService.Update(diagnosis);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(model);

        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {

            var diagnosis = await _diagnosisMasterService.Get(id);

            if (diagnosis == null)
                return BadRequest();

            diagnosis.IsDeleted = true;

            diagnosis.ModifiedDate = DateTime.Now;
            diagnosis.ModifiedBy = ActiveUser.Id;

            await _diagnosisMasterService.Update(diagnosis);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok();
        }

        #endregion Diagnosis Ends
    }
}