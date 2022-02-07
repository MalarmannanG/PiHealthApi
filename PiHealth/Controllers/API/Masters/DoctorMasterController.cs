using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PiHealth.DataModel.Entity;
using PiHealth.Service.UserAccounts;
using PiHealth.Services.Master;
using PiHealth.Services.UserAccounts;
using PiHealth.Web.MappingExtention;
using PiHealth.Web.Model.DoctorMaster;
using PiHealth.Controllers;
using PiHealth.Services;
using PiHealth.Web.Filters;

namespace PiHealth.Web.Controllers.API.Masters
{
    [Authorize]
    [Route("Api/[Controller]")]
    [Produces("application/json")]
    public class DoctorMasterController : BaseApiController
    {

        private readonly DoctorMasterService _doctorMasterService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly AuditLogServices _auditLogService;

        public DoctorMasterController(DoctorMasterService doctorMasterService, IAppUserService appUserService, IWebHostEnvironment hostingEnvironment, AuditLogServices auditLogServices)
        {
            _doctorMasterService = doctorMasterService;                      
            _hostingEnvironment = hostingEnvironment;
            _auditLogService = auditLogServices;
        }

        [HttpGet]
        [Route("Get/{id}")]        
        public IActionResult Get(long id)
        {
            var doctorMaster = _doctorMasterService.Get(id)?.Result?.ToModel(new DoctorMasterModel());
            return Ok(doctorMaster);
        }

        [HttpGet]
        [Route("GetAll")]        
        public IActionResult GetAll([FromQuery] DoctorQueryModel model)
        {
            var doctorMasters = _doctorMasterService.GetAll(name: model?.name);
            var total = doctorMasters.Count();
            var orderBy = string.IsNullOrEmpty(model?.order_by) ? "CreatedDate" : model.order_by;
            doctorMasters = doctorMasters?.OrderByDescending(a => a.CreatedDate).Skip(model.skip);
            if (model.take > 0)
            {
                doctorMasters = doctorMasters.Take(model.take);
            }
            var result = doctorMasters?.ToList().Select(a => a.ToModel(new DoctorMasterModel())).ToList();
            return Ok(new { result, total});
        }

        [HttpGet]        
        [Route("AutoComplete")]
        public IActionResult AutoComplete(string name = null)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                var doctorMasters = _doctorMasterService.AutoComplete(name: name).ToList().Select(a => a.ToModel(new DoctorMasterModel())).ToList();
                return Ok(doctorMasters);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("Create")]        
        public async Task<IActionResult> Create([FromBody] DoctorMasterModel model)
        {
            if (model == null)
                return BadRequest();

            var entity = model.ToEntity(new DoctorMaster());
            entity.CreatedBy = ActiveUser.Id;
            entity.CreatedDate = DateTime.Now;

            await _doctorMasterService.Create(entity);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(new { status = true, msg = "Doctor Info added successfully" });
        }

        [HttpPut]
        [Route("Update")]        
        public async Task<IActionResult> Update([FromBody] DoctorMasterModel model)
        {
            if (model == null)
                return BadRequest();

            var entity = await _doctorMasterService.Get(model.id);

            if (entity == null)
                return BadRequest();

            entity.ModifiedBy = ActiveUser.Id;
            entity.ModifiedDate = DateTime.Now;
            entity.Name = model.name;
            entity.Address = model.address;
            entity.ClinicName = model.clinicName;
            entity.Email = model.email;
            entity.Percentage = model.percentage;
            entity.TelNo = model.telNo;
            entity.PhoneNo1 = model.phoneNo1;
            entity.PhoneNo2 = model.phoneNo2;
            entity.Qualification = model.qualification;

            await _doctorMasterService.Update(entity);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(new { status = true, msg = "Doctor Info updated successfully" });
        }
        
        [HttpDelete]
        [Route("Delete/{id}")]        
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await _doctorMasterService.Get(id);

            if (entity == null)
                return BadRequest();

            entity.IsDeleted = true;
            entity.ModifiedBy = ActiveUser.Id;
            entity.ModifiedDate = DateTime.Now;

            await _doctorMasterService.Update(entity);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(new { status = true, msg = "Doctor Info deleted successfully" });
        }
    }
}