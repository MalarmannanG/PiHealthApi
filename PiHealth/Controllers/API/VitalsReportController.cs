using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.Web.Filters;
using PiHealth.DataModel.Entity;
using PiHealth.Service.UserAccounts;
using PiHealth.Services;
using PiHealth.Services.Master;
using PiHealth.Web.Helper;
using PiHealth.Web.MappingExtention;
using PiHealth.Web.Model.VitalsReport;
using PiHealth.Controllers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Microsoft.AspNetCore.StaticFiles;

namespace PiHealth.Web.Controllers.API.Masters
{
    [Authorize]
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class VitalsReportController : BaseApiController
    {
        private readonly VitalsReportService _vitalsReportService;
        private readonly IAppUserService _appUserService;
        private readonly AppointmentService _appointmentService;
        private readonly AuditLogServices _auditLogService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public VitalsReportController(
            IHostingEnvironment hostingEnvironment,
            VitalsReportService vitalsReportService,
            AppointmentService appointmentService,
            IAppUserService appUserService,
            AuditLogServices auditLogServices)
        {
            _vitalsReportService = vitalsReportService;
            _appUserService = appUserService;
            _auditLogService = auditLogServices;
            _appointmentService = appointmentService;
            _hostingEnvironment = hostingEnvironment;
        }

        #region  VitalsReport Master

        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult Get(long id)
        {
            var vitalsReport = _vitalsReportService.GetByAppointmentId(id)?.ToModel(new VitalsReportModel()) ?? new VitalsReportModel();
            return Ok(vitalsReport);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var vitalsReports = _vitalsReportService.GetActive().OrderByDescending(a => a.CreatedDate).ToList().Select(a => a.ToModel(new VitalsReportModel())).ToList();
            return Ok(vitalsReports);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] VitalsReportModel model)
        {
            var vitalsReport = model.ToEntity(new VitalsReport());
            vitalsReport.CreatedDate = DateTime.Now;
            vitalsReport.CreatedBy = ActiveUser.Id;
            _vitalsReportService.Add(vitalsReport);
            

            if (model.patientNewFiles?.Count > 0)
            {
                var appointment = await _appointmentService.Get(model.appointmentID.Value);

                foreach (var item in model.patientNewFiles)
                {
                    var fileByte = Convert.FromBase64String(item.base64String);
                    string baseDir =  "Reports";
                    item.fileName = item.fileName;
                    string fileFullName = Path.Combine(baseDir, Guid.NewGuid().ToString().Substring(0, 5) + item.fileName);
                    PiHealthFileHelper.Save(fileByte, fileFullName);
                    appointment.PatientFiles.Add(new PatientFiles()
                    {
                        AppointmentID = model.appointmentID.Value,
                        PatientID = model.patientID.Value,
                        FileName = item.fileName,
                        FilePath = fileFullName,
                        CreatedBy = ActiveUser.Id,
                        CreatedDate = DateTime.Now
                    });
                }
            }


            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(vitalsReport);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] VitalsReportModel model)
        {
            if (model == null)
                return BadRequest();

            var vitalsReport = _vitalsReportService.Get(model.id);

            if (vitalsReport == null)
                return BadRequest();

            vitalsReport = model.ToEntity(vitalsReport);
            _vitalsReportService.Update(vitalsReport);


            if (model.patientNewFiles?.Count > 0)
            {
                var appointment = await _appointmentService.Get(model.appointmentID.Value);

                foreach (var item in model.patientNewFiles)
                {
                    var fileByte = Convert.FromBase64String(item.base64String);
                    string baseDir = "Reports";
                    item.fileName =  item.fileName;
                    string fileFullName = Path.Combine(baseDir, Guid.NewGuid().ToString().Substring(0, 5) + item.fileName);
                    PiHealthFileHelper.Save(fileByte, fileFullName);
                    appointment.PatientFiles.Add(new PatientFiles()
                    {
                        AppointmentID = model.appointmentID.Value,
                        PatientID = model.patientID.Value,
                        FileName = item.fileName,
                        FilePath = fileFullName,
                        CreatedBy = ActiveUser.Id,
                        CreatedDate = DateTime.Now
                    });
                }
            }

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok(model);

        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            if (id == 0)
                return BadRequest();

            var vitalsReport = _vitalsReportService.Get(id);

            if (vitalsReport == null)
                return BadRequest();

            vitalsReport.IsActive = false;

            vitalsReport.UpdatedDate = DateTime.Now;
            vitalsReport.UpdatedBy = ActiveUser.Id;

            _vitalsReportService.Update(vitalsReport);

            _auditLogService.InsertLog(ControllerName: ControllerName, ActionName: ActionName, UserAgent: UserAgent, RequestIP: RequestIP, userid: ActiveUser.Id, value1: "Success");

            return Ok();
        }

        [HttpPost]
        [Route("GetFile")]
        public IActionResult GetFile([FromBody] GetFileInput input)
        {
            if (input == null)
                return BadRequest();
            var bytes = new FileStream(input.filePath, FileMode.Open, FileAccess.Read);
            string contentType = "";
            new FileExtensionContentTypeProvider().TryGetContentType(input.fileName, out contentType);
            return File(bytes, contentType, input.fileName);
           
        }
        #endregion VitalsReport Ends
    }
}