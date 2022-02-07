using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.Web.Filters;
using PiHealth.DataModel.Entity;
using PiHealth.Service.UserAccounts;
using PiHealth.Services;
using PiHealth.Services.Master;
using PiHealth.Services.PiHealthPatients;
using PiHealth.Web.MappingExtention;
using PiHealth.Web.Model.Appointment;
using PiHealth.Controllers;

namespace PiHealth.Web.Controllers.API
{
    [Authorize]
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class AppointmentController : BaseApiController
    {
        private readonly AppointmentService _appointmentService;
        private readonly IAppUserService _appUserService;
        private readonly AuditLogServices _auditLogService;
        private readonly PatientService _patientService;

        public AppointmentController(
            PatientService patientService,
            AppointmentService appointmentService,
            IAppUserService appUserService,
            AuditLogServices auditLogServices)
        {
            _appointmentService = appointmentService;
            _appUserService = appUserService;
            _auditLogService = auditLogServices;
            _patientService = patientService;
        }

        #region  Appointment Master

        [HttpGet]
        [Route("Dashboard")]
        public async Task<IActionResult> Dasboard(long id)
        {
            DateTime today = DateTime.Now.Date;
            DateTime fromDate = today;
            DateTime toDate = today.AddDays(1).AddMilliseconds(-1);
            var todaysPatients = _appointmentService.GetAll(fromDate: fromDate, toDate: toDate).Count();
            var todaysProcedures = _appointmentService.GetAll(fromDate: fromDate, toDate: toDate, isProcedure:true).Count();
            var todaysAppointments = _appointmentService.GetAll(fromDate: fromDate, toDate: toDate, isProcedure:false).Count();
            return Ok(new { todaysPatients, todaysAppointments, todaysProcedures });
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var appointment = await _appointmentService.Get(id);
            var result = appointment.ToModel(new AppointmentModel());
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll([FromQuery] AppointmentQueryModel model)
        {
            var patients1 = new List<long>();
            var patients2 = new List<long>();
            long[] patientIds = null;
            long[] doctorIds = null;

            if (model.todayPatients)
            {
                patients1 = _patientService.GetAll(isTodayPatients: model.todayPatients).Select(a => a.Id).ToList();
                if (patients1?.Count() == 0)
                {                    
                    return Ok(new { result = new List<AppointmentModel>(), total = 0 });
                }
            }

            if (!string.IsNullOrEmpty(model.patientName))
            {
                patients2 = _patientService.GetAll(name: model.patientName).Select(a => a.Id).ToList();
            }

            if (!string.IsNullOrEmpty(model.doctorName))
            {
                doctorIds = _appUserService.GetAll(name: model.doctorName).Select(a => a.Id).ToArray();
            }

            if (patients1?.Count() > 0 && patients2?.Count() > 0)
            {
                patients1.AddRange(patients2);
                patientIds = patients1.ToArray();
            }
            else if (patients1?.Count() > 0)
            {
                patientIds = patients1.ToArray();
            }
            else if (patients2?.Count() > 0)
            {
                patientIds = patients2.ToArray();
            }

            var appointments = _appointmentService.GetAll(patientIds: patientIds, doctorIds: doctorIds, isProcedure: model?.isProcedure, fromDate: model.fromDate, toDate: model.toDate);            
            var total = appointments?.Count();
            var orderBy = string.IsNullOrEmpty(model?.order_by) ? "CreatedDate" : model.order_by;
            appointments = appointments?.OrderByDescending(a => a.CreatedDate).Skip(model.skip);

            if (model.take > 0)
            {
                appointments = appointments.Take(model.take);
            }

            var result = appointments.ToList().Select(a => a.ToModel(new AppointmentModel())).ToList() ?? new List<AppointmentModel>();
            return Ok(new { result, total });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] AppointmentModel model)
        {
            var appointment = model.ToEntity(new Appointment());            
            appointment.CreatedDate = DateTime.Now;
            appointment.CreatedBy = ActiveUser.Id;
            appointment.IsActive = true;
            await _appointmentService.Create(appointment);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(appointment);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] AppointmentModel model)
        {
            if (model == null)
                return BadRequest();

            var appointment = await _appointmentService.Get(model.id);

            if (appointment == null)
                return BadRequest();

            var updated = model.ToEntity(appointment);
            appointment = await _appointmentService.Update(appointment);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(model);

        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var appointment = await _appointmentService.Get(id);

            if (appointment == null)
                return BadRequest();

            appointment.IsActive = false;
            appointment.UpdatedDate = DateTime.Now;
            appointment.UpdatedBy = ActiveUser.Id;

            await _appointmentService.Update(appointment);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok();
        }

        #endregion Appointment Ends
    }
}