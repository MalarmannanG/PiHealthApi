using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.Web.Filters;
using PiHealth.DataModel.Entity;
using PiHealth.Service.UserAccounts;
using PiHealth.Services;
using PiHealth.Services.Master;
using PiHealth.Web.MappingExtention;
using PiHealth.Web.Model.TestMaster;
using PiHealth.Controllers;

namespace PiHealth.Web.Controllers.API.Masters
{
    [Authorize]
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class TestMasterController : BaseApiController
    {
        private readonly TestMasterService _testMasterService;
        private readonly IAppUserService _appUserService;
        private readonly AuditLogServices _auditLogService;

        public TestMasterController(
            TestMasterService testMasterService,
            IAppUserService appUserService,
            AuditLogServices auditLogServices)
        {
            _testMasterService = testMasterService;
            _appUserService = appUserService;
            _auditLogService = auditLogServices;
        }

        #region  Test Master

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _testMasterService.Get(id);
            var test = result.ToModel(new TestMasterModel());
            return Ok(test);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll([FromQuery] TestMasterQueryModel model)
        {
            var entities = _testMasterService.GetAll(name: model.name);
            var total = entities.Count();
            entities = entities.OrderByDescending(a => a.CreatedDate).Skip(model.skip);
            if (model.take > 0)
            {
                entities = entities.Take(model.take);
            }
            var result = entities.ToList()?.Select(a => a.ToModel(new TestMasterModel())).ToList();
            return Ok(new { result, total });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] TestMasterModel model)
        {
            var test = model.ToEntity(new TestMaster());
            test.CreatedDate = DateTime.Now;
            test.CreatedBy = ActiveUser.Id;
            await _testMasterService.Create(test);
            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(test);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] TestMasterModel model)
        {
            if (model == null)
                return BadRequest();

            var test = await _testMasterService.Get(model.id.Value);

            if (test == null)
                return BadRequest();

            test.Name = model.name;
            test.Remarks = model.remarks;
            test.IsDeleted = model.isDeleted;
            test.ModifiedDate = DateTime.Now;
            test.ModifiedBy = ActiveUser.Id;
            await _testMasterService.Update(test);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(model);

        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {

            var test = await _testMasterService.Get(id);

            if (test == null)
                return BadRequest();

            test.IsDeleted = true;

            test.ModifiedDate = DateTime.Now;
            test.ModifiedBy = ActiveUser.Id;

            await _testMasterService.Update(test);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok();
        }

        #endregion Test Ends
    }
}