using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.DataModel.Entity;
using PiHealth.Web.Model.VitalsReport;

namespace PiHealth.Web.MappingExtention
{
    public static class VitalsReportMapping
    {
        public static VitalsReportModel ToModel(this VitalsReport entity, VitalsReportModel model)
        {
            model.height = entity.Height;
            model.weight = entity.Weight;
            model.pulse = entity.Pulse;
            model.bp = entity.BP;
            model.bloodPresure = entity.BloodPresure;
            model.spO2 = entity.SpO2;
            model.appointmentID = entity.AppointmentID;
            model.patientID = entity.PatientID;
            model.temprature = entity.Temprature;
            model.id = entity.Id;
            model.isActive = entity.IsActive;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.updatedBy = entity.UpdatedBy;
            model.updatedDate = entity.UpdatedDate;
            model.patientFiles = entity.Appointment.PatientFiles.Select(a => a.ToModel(new PatientFilesModel())).ToList();
            return model;
        }

        public static VitalsReport ToEntity(this VitalsReportModel model, VitalsReport entity)
        {
            entity.AppointmentID = model.appointmentID;
            entity.BloodPresure = model.bloodPresure;
            entity.PatientID = model.patientID;
            entity.Height = model.height;
            entity.Weight = model.weight;            
            entity.Pulse = model.pulse;
            entity.BP = model.bp;
            entity.SpO2 = model.spO2;
            entity.IsActive = model.isActive;
            entity.Temprature = model.temprature;            
            entity.CreatedBy =model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.UpdatedBy = model.updatedBy;
            entity.UpdatedDate = model.updatedDate;
            return entity;
        }

        public static PatientFilesModel ToModel(this PatientFiles entity, PatientFilesModel model)
        {
            
            model.id = entity.Id;
            model.appointmentID = entity.AppointmentID;
            model.patientID = entity.PatientID;
            model.filePath = entity.FilePath;
            model.fileName = entity.FileName;
            model.isDeleted = entity.IsDeleted;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.updatedBy = entity.UpdatedBy;
            model.updatedDate = entity.UpdatedDate;
            return model;
        }

        public static PatientFiles ToEntity(this PatientFilesModel model, PatientFiles entity)
        {            
            entity.AppointmentID = model.appointmentID;
            entity.PatientID = model.patientID;
            entity.FilePath = model.filePath;
            entity.FileName = model.fileName;
            entity.IsDeleted = model.isDeleted;
            entity.CreatedBy = model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.UpdatedBy = model.updatedBy;
            entity.UpdatedDate = model.updatedDate;
            return entity;
        }
    }
}
