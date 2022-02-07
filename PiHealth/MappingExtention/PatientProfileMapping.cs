using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.DataModel.Entity;
using PiHealth.Web.Model.Appointment;
using PiHealth.Web.Model.PatientProfile;

namespace PiHealth.Web.MappingExtention
{
    public static class PatientProfileMapping
    {
        public static PatientProfileModel ToModel(this PatientProfile entity, PatientProfileModel model)
        {
            model.id = entity.Id;
            model.patientId = entity.PatientId;
            model.doctorId = entity.DoctorId;
            model.appointmentId = entity.AppointmentId;
            model.compliants = entity.Compliants;
            model.examination = entity.Examination;
            model.impression = entity.Impression;
            model.advice = entity.Advice;
            model.templateMasterId = entity.TemplateMasterId;
            model.plan = entity.Plan;
            model.followUp = entity.FollowUp;
            model.isDeleted = entity.IsDeleted;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.modifiedBy = entity.ModifiedBy;
            model.modifiedDate = entity.ModifiedDate;
            model.patientModel = entity.Patient?.ToModel(new Model.Patient.PatientModel());
            model.appointment = entity.Appointment?.ToModel(new AppointmentModel());
            model.prescriptionModel = entity.Prescriptions?.ToList()?.Select(a => a.ToModel(new PrescriptionModel())).ToList() ?? new List<PrescriptionModel>();
            model.patientDiagnosisModel = entity.PatientDiagnosis?.ToList()?.Select(a => a.ToModel(new PatientDiagnosisModel())).ToList() ?? new List<PatientDiagnosisModel>();
            model.patientTestModel = entity.PatientTests?.ToList()?.Select(a => a.ToModel(new PatientTestModel())).ToList() ?? new List<PatientTestModel>();
            return model;
        }

        public static PatientProfile ToEntity(this PatientProfileModel model, PatientProfile entity)
        {
            entity.Compliants = model.compliants;
            entity.Examination = model.examination;
            entity.Impression = model.impression;
            entity.TemplateMasterId = model.templateMasterId;
            entity.Advice = model.advice;
            entity.Plan = model.plan;
            entity.FollowUp = model.followUp;
            entity.IsDeleted = model.isDeleted;
            entity.CreatedBy =model.createdBy;            
            entity.CreatedDate = model.createdDate;
            entity.ModifiedBy = model.modifiedBy;
            entity.ModifiedDate = model.modifiedDate;
            entity.PatientId = model.patientId;
            entity.AppointmentId = model.appointmentId;
            entity.Prescriptions = model.prescriptionModel?.ToList()?.Select(a => a.ToEntity(new Prescription())).ToList() ?? new List<Prescription>();
            entity.PatientDiagnosis = model.patientDiagnosisModel?.ToList()?.Select(a => a.ToEntity(new PatientDiagnosis())).ToList() ?? new List<PatientDiagnosis>();
            entity.PatientTests = model.patientTestModel?.ToList()?.Select(a => a.ToEntity(new PatientTest())).ToList() ?? new List<PatientTest>();
            return entity;
        }


        public static PrescriptionModel ToModel(this Prescription entity, PrescriptionModel model)
        {
            model.id = entity.Id;
            model.patientProfileId = entity.PatientProfileId;
            model.medicinName = entity.MedicinName;
            model.genericName = entity.GenericName;
            model.strength = entity.Strength;
            model.beforeFood = entity.BeforeFood;
            model.morning = entity.Morning;
            model.noon = entity.Noon;
            model.night = entity.Night;
            model.description = entity.Description;
            model.noOfDays = entity.NoOfDays;
            model.isDeleted = entity.IsDeleted;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.modifiedBy = entity.ModifiedBy;
            model.modifiedDate = entity.ModifiedDate;
            return model;
        }

        public static Prescription ToEntity(this PrescriptionModel model, Prescription entity)
        {
            entity.PatientProfileId = model.patientProfileId;
            entity.MedicinName = model.medicinName;
            entity.GenericName = model.genericName;
            entity.Strength = model.strength;
            entity.BeforeFood = model.beforeFood;
            entity.Morning = model.morning;
            entity.Noon = model.noon;
            entity.Night = model.night;
            entity.Description = model.description;
            entity.NoOfDays = model.noOfDays;
            entity.IsDeleted = model.isDeleted;
            entity.CreatedBy = model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.ModifiedBy = model.modifiedBy;
            entity.ModifiedDate = model.modifiedDate;
            return entity;
        }

        public static PatientTestModel ToModel(this PatientTest entity, PatientTestModel model)
        {
            model.id = entity.Id;
            model.patientProfileId = entity.PatientProfileId;
            model.testMasterId = entity.TestMasterId;
            model.testMasterName = entity.TestMaster?.Name;            
            model.remarks = entity.Remarks;
            model.isDeleted = entity.IsDeleted;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.modifiedBy = entity.ModifiedBy;
            model.modifiedDate = entity.ModifiedDate;
            return model;
        }

        public static PatientTest ToEntity(this PatientTestModel model, PatientTest entity)
        {
            entity.PatientProfileId = model.patientProfileId;
            entity.TestMasterId = model.testMasterId;
            entity.Remarks = model.remarks;
            entity.IsDeleted = model.isDeleted;
            entity.CreatedBy = model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.ModifiedBy = model.modifiedBy;
            entity.ModifiedDate = model.modifiedDate;
            return entity;
        }

        public static PatientDiagnosisModel ToModel(this PatientDiagnosis entity, PatientDiagnosisModel model)
        {
            model.id = entity.Id;
            model.patientProfileId = entity.PatientProfileId;
            model.diagnosisMasterId = entity.DiagnosisMasterId;
            model.diagnosisMasterName = entity.DiagnosisMaster?.Name;            
            model.description = entity.Description;
            model.isDeleted = entity.IsDeleted;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.modifiedBy = entity.ModifiedBy;
            model.modifiedDate = entity.ModifiedDate;
            return model;
        }

        public static PatientDiagnosis ToEntity(this PatientDiagnosisModel model, PatientDiagnosis entity)
        {
            entity.PatientProfileId = model.patientProfileId;
            entity.DiagnosisMasterId = model.diagnosisMasterId;
            entity.Description = model.description;
            entity.IsDeleted = model.isDeleted;
            entity.CreatedBy = model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.ModifiedBy = model.modifiedBy;
            entity.ModifiedDate = model.modifiedDate;
            return entity;
        }
    }
}
