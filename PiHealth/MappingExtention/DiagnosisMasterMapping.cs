using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.DataModel.Entity;
using PiHealth.Web.Model.DiagnosisMaster;

namespace PiHealth.Web.MappingExtention
{
    public static class DiagnosisMasterMapping
    {
        public static DiagnosisMasterModel ToModel(this DiagnosisMaster entity, DiagnosisMasterModel model)
        {
            model.name = entity.Name;
            model.description = entity.Description;
            model.isDeleted = entity.IsDeleted;
            model.id = entity.Id;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.updatedBy = entity.ModifiedBy;
            model.updatedDate = entity.ModifiedDate;
            return model;
        }

        public static DiagnosisMaster ToEntity(this DiagnosisMasterModel model, DiagnosisMaster entity)
        {
            entity.Name = model.name;
            entity.Description = model.description;
            entity.Id = model.id ?? 0;
            entity.IsDeleted = model.isDeleted;
            entity.CreatedBy =model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.ModifiedBy = model.updatedBy;
            entity.ModifiedDate = model.updatedDate;
            return entity;
        }
    }
}
