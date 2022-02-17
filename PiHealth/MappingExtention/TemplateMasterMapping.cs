using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.DataModel.Entity;
using PiHealth.Web.Model.TemplateMaster;

namespace PiHealth.Web.MappingExtention
{
    public static class TemplateMasterMapping
    {
        public static TemplateMasterModel ToModel(this TemplateMaster entity, TemplateMasterModel model)
        {
            model.id = entity.Id;
            model.name = entity.Name;
            model.description = entity.Description;
            model.compliants = entity.Compliants;
            model.examination = entity.Examination;
            model.impression = entity.Impression;
            model.advice = entity.Advice;
            model.plan = entity.Plan;
            model.followUp = entity.FollowUp;
            model.isDeleted = entity.IsDeleted;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.modifiedBy = entity.ModifiedBy;
            model.modifiedDate = entity.ModifiedDate;
            model.templatePrescriptionModel = entity.TemplatePrescriptions?.ToList()?.Select(a => a.ToModel(new TemplatePrescriptionModel())).ToList() ?? new List<TemplatePrescriptionModel>();
            return model;
        }

        public static TemplateMaster ToEntity(this TemplateMasterModel model, TemplateMaster entity)
        {
            entity.Name = model.name;
            entity.Description = model.description;
            entity.Compliants = model.compliants;
            entity.Examination = model.examination;
            entity.Impression = model.impression;
            entity.Advice = model.advice;
            entity.Plan = model.plan;
            entity.FollowUp = model.followUp;
            entity.IsDeleted = model.isDeleted;
            entity.CreatedBy =model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.ModifiedBy = model.modifiedBy;
            entity.ModifiedDate = model.modifiedDate;
            entity.TemplatePrescriptions = model.templatePrescriptionModel?.ToList()?.Select(a => a.ToEntity(new TemplatePrescription())).ToList() ?? new List<TemplatePrescription>();
            return entity;
        }


        public static TemplatePrescriptionModel ToModel(this TemplatePrescription entity, TemplatePrescriptionModel model)
        {
            model.id = entity.Id;
            model.templateMasterId = entity.TemplateMasterId;
            model.medicineName = entity.MedicineName;
            model.strength = entity.Strength;
            model.beforeFood = entity.BeforeFood;
            model.morning = entity.Morning;
            model.noon = entity.Noon;
            model.night = entity.Night;
            model.remarks = entity.Remarks;
            model.noOfDays = entity.NoOfDays;
            model.isDeleted = entity.IsDeleted;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.modifiedBy = entity.ModifiedBy;
            model.modifiedDate = entity.ModifiedDate;
            return model;
        }

        public static TemplatePrescription ToEntity(this TemplatePrescriptionModel model, TemplatePrescription entity)
        {
            entity.TemplateMasterId = model.templateMasterId;
            entity.MedicineName = model.medicineName;
            entity.Strength = model.strength;
            entity.BeforeFood = model.beforeFood;
            entity.Morning = model.morning;
            entity.Noon = model.noon;
            entity.Night = model.night;
            entity.Remarks = model.remarks;
            entity.NoOfDays = model.noOfDays;
            entity.IsDeleted = model.isDeleted;
            entity.CreatedBy = model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.ModifiedBy = model.modifiedBy;
            entity.ModifiedDate = model.modifiedDate;
            return entity;
        }
    }
}
