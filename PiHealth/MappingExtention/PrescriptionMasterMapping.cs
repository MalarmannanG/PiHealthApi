using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.DataModel.Entity;
using PiHealth.Web.Model.PrescriptionMaster;

namespace PiHealth.Web.MappingExtention
{
    public static class PrescriptionMasterMapping
    {
        public static PrescriptionMasterModel ToModel(this PrescriptionMaster entity, PrescriptionMasterModel model)
        {
            model.medicinName = entity.MedicinName;
            model.genericName = entity.GenericName;
            model.categoryName = entity.CategoryName;
            model.strength = entity.Strength;
            model.units = entity.Units;
            model.description = entity.Description;
            model.isDeleted = entity.IsDeleted;
            model.id = entity.Id;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.modifiedBy = entity.ModifiedBy;
            model.modifiedDate = entity.ModifiedDate;
            return model;
        }

        public static PrescriptionMaster ToEntity(this PrescriptionMasterModel model, PrescriptionMaster entity)
        {
            entity.MedicinName = model.medicinName;
            entity.GenericName = model.genericName;
            entity.CategoryName = model.categoryName;
            entity.Strength = model.strength;
            entity.Units = model.units;
            entity.Description = model.description;
            entity.Id = model.id;
            entity.IsDeleted = model.isDeleted;
            entity.CreatedBy =model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.ModifiedBy = model.modifiedBy;
            entity.ModifiedDate = model.modifiedDate;
            return entity;
        }
    }
}
