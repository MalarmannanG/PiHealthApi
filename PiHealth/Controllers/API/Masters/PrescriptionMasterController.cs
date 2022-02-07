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
using PiHealth.Web.Model.PrescriptionMaster;
using PiHealth.Controllers;

namespace PiHealth.Web.Controllers.API.Masters
{
    [Authorize]
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class PrescriptionMasterController : BaseApiController
    {
        private readonly PrescriptionMasterService _prescriptionMasterService;
        private readonly IAppUserService _appUserService;
        private readonly AuditLogServices _auditLogService;

        public PrescriptionMasterController(
            PrescriptionMasterService prescriptionMasterService,
            IAppUserService appUserService,
            AuditLogServices auditLogServices)
        {
            _prescriptionMasterService = prescriptionMasterService;
            _appUserService = appUserService;
            _auditLogService = auditLogServices;
        }

        #region  PrescriptionMaster Master

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _prescriptionMasterService.Get(id);
            var diagnosis = result.ToModel(new PrescriptionMasterModel());
            return Ok(diagnosis);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll([FromQuery] PrescriptionMasterQueryModel model)
        {
            var entities = _prescriptionMasterService.GetAll(name: model.name);
            var total = entities.Count();
            entities = entities.OrderByDescending(a => a.CreatedDate).Skip(model.skip);
            if (model.take > 0)
            {
                entities = entities.Take(model.take);
            }
            var result = entities.ToList()?.Select(a => a.ToModel(new PrescriptionMasterModel())).ToList();
            return Ok(new { result, total });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] PrescriptionMasterModel model)
        {
            var diagnosis = model.ToEntity(new PrescriptionMaster());
            diagnosis.CreatedDate = DateTime.Now;
            diagnosis.CreatedBy = ActiveUser.Id;
            await _prescriptionMasterService.Create(diagnosis);
            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(diagnosis);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] PrescriptionMasterModel model)
        {
            if (model == null)
                return BadRequest();

            var diagnosis = await _prescriptionMasterService.Get(model.id);

            if (diagnosis == null)
                return BadRequest();

            diagnosis = model.ToEntity(diagnosis);
            await _prescriptionMasterService.Update(diagnosis);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(model);

        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {

            var diagnosis = await _prescriptionMasterService.Get(id);

            if (diagnosis == null)
                return BadRequest();

            diagnosis.IsDeleted = true;

            diagnosis.ModifiedDate = DateTime.Now;
            diagnosis.ModifiedBy = ActiveUser.Id;

            await _prescriptionMasterService.Update(diagnosis);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok();
        }

        #endregion PrescriptionMaster Ends
    }
}