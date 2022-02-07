using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.DataModel.Entity;
using PiHealth.Web.Model.Appointment;
using PiHealth.Web.Model.Patient;
using PiHealth.Web.Model.VitalsReport;

namespace PiHealth.Web.MappingExtention
{
    public static class AppointmentMapping
    {
        public static AppointmentModel ToModel(this Appointment entity, AppointmentModel model)
        {
            model.id = entity.Id;
            model.patientId = entity.PatientId;
            model.patientName = (entity.Patient?.Initial != null ? entity.Patient.Initial + " " : "") + entity.Patient?.PatientName; 
            model.consultingDoctorName = entity.AppUser?.Name;
            model.consultingDoctorID = entity.AppUserId;
            model.patient = entity.Patient?.ToModel(new PatientModel());
            model.visitType = entity.VisitType;
            model.appointmentDateTime = entity.AppointmentDateTime;
            model.dayOrNight = entity.DayOrNight;
            model.timeOfAppintment = entity.TimeOfAppintment;
            model.description = entity.Description;
            model.id = entity.Id;
            model.isActive = entity.IsActive;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.updatedBy = entity.UpdatedBy;
            model.updatedDate = entity.UpdatedDate;
            model.vitalsReportModel = entity.VitalsReport?.ToModel(new VitalsReportModel());
            model.patientFiles = entity.PatientFiles.Select(a => a.ToModel(new PatientFilesModel())).ToList();
            return model;
        }       

        public static Appointment ToEntity(this AppointmentModel model, Appointment entity)
        {
            entity.PatientId = model.patientId;
            entity.AppUserId = model.consultingDoctorID;
            entity.VisitType = model.visitType;
            entity.TimeOfAppintment = model.timeOfAppintment;
            entity.DayOrNight = model.dayOrNight;
            entity.AppointmentDateTime = model.appointmentDateTime;
            entity.Description = model.description;
            entity.Id = model.id;
            entity.IsActive = model.isActive;
            entity.CreatedBy =model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.UpdatedBy = model.updatedBy;
            entity.UpdatedDate = model.updatedDate;
            return entity;
        }
    }
}
