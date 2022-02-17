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
using PiHealth.Services.PatientProfileService;
using PiHealth.Web.MappingExtention;
using PiHealth.Web.Model.Appointment;
using PiHealth.Web.Model.PatientProfile;
using PiHealth.Web.Model.VitalsReport;
using PiHealth.Controllers;

namespace PiHealth.Web.Controllers.API
{
    [Authorize]
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class PatientProfileController : BaseApiController
    {
        private readonly PatientProfileService _patientProfileService;
        private readonly IAppUserService _appUserService;
        private readonly AuditLogServices _auditLogService;
        private readonly AppointmentService _appointmentService;

        public PatientProfileController(
            PatientProfileService patientProfileService,
            IAppUserService appUserService,
            AppointmentService appointmentService,
            AuditLogServices auditLogServices)
        {
            _patientProfileService = patientProfileService;
            _appUserService = appUserService;
            _appointmentService = appointmentService;
            _auditLogService = auditLogServices;
        }

        #region  PatientProfile Master

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _patientProfileService.Get(id);
            var patientProfile = result?.ToModel(new PatientProfileModel()) ?? new PatientProfileModel();
            if (result == null)
            {
                var appointment = await _appointmentService.Get(id);
                patientProfile.patientModel = appointment?.Patient?.ToModel(new Model.Patient.PatientModel());
                patientProfile.appointment = appointment?.ToModel(new AppointmentModel());
                patientProfile.appointment.patientFiles = appointment?.PatientFiles?.Select(a => a.ToModel(new PatientFilesModel())).ToList();                
                patientProfile.appointmentId = appointment.Id;
                patientProfile.patientId = appointment.PatientId ?? 0;
            }
            return Ok(patientProfile);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll([FromQuery] PatientProfileQueryModel model)
        {
            var appointments = _appointmentService.GetAll().Where(a => a.PatientId == model.PatientId && a.AppointmentDateTime < model.appointmentDate).ToList();
            var appointmentIds = appointments.Select(a => a.Id).ToArray();
            var patientProfiles = _patientProfileService.GetAll(patientId: model.PatientId, appointmentIds: appointmentIds).OrderByDescending(a => a.CreatedDate).ToList().Select(a => a.ToModel(new PatientProfileModel())).ToList();
            return Ok(patientProfiles);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] PatientProfileModel model)
        {
            var patientProfile = model.ToEntity(new PatientProfile());
            patientProfile.CreatedDate = DateTime.Now;
            patientProfile.CreatedBy = ActiveUser.Id;
            await _patientProfileService.Create(patientProfile);
            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(patientProfile);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] PatientProfileModel model)
        {
            if (model == null)
                return BadRequest();

            var patientProfile = await _patientProfileService.Get(model.appointmentId);

            try
            {
                if (patientProfile == null)
                {                    
                    patientProfile = model.ToEntity(new PatientProfile());
                    patientProfile.CreatedDate = DateTime.Now;
                    patientProfile.CreatedBy = ActiveUser.Id;
                    if (!model.appointment.isActive)
                    {
                        var _appoinment = await _appointmentService.Get(model.appointmentId);
                        _appoinment.IsActive = false;
                        _appoinment.UpdatedBy = ActiveUser.Id;
                        _appoinment.UpdatedDate = DateTime.Now;
                        await _appointmentService.Update(_appoinment);
                    }
                    await _patientProfileService.Create(patientProfile);
                }
                else
                {

                    patientProfile = model.ToEntity(patientProfile);
                    patientProfile.IsDeleted = model.isDeleted;
                    patientProfile.ModifiedDate = DateTime.Now;
                    patientProfile.ModifiedBy = ActiveUser.Id;
                      if (!model.appointment.isActive)
                    {
                        var _appoinment = await _appointmentService.Get(model.appointmentId);
                        _appoinment.IsActive = false;
                        _appoinment.UpdatedBy = ActiveUser.Id;
                        _appoinment.UpdatedDate = DateTime.Now;
                        await _appointmentService.Update(_appoinment);
                    }
                    await _patientProfileService.Update(patientProfile);
                    _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(model);

        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {

            var patientProfile = await _patientProfileService.Get(id);

            if (patientProfile == null)
                return BadRequest();

            patientProfile.IsDeleted = true;

            patientProfile.ModifiedDate = DateTime.Now;
            patientProfile.ModifiedBy = ActiveUser.Id;

            await _patientProfileService.Update(patientProfile);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok();
        }

        #endregion PatientProfile Ends
    }
}