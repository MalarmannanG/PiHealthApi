using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.DataModel.Entity;
using PiHealth.Web.Model.Department;

namespace PiHealth.Web.MappingExtention
{
    public static class DepartmentMasterMapping
    {
        public static DepartmentMasterModel ToModel(this DepartmentMaster entity, DepartmentMasterModel model)
        {
            model.name = entity.Name;
            model.description = entity.Description;
            model.isDeleted = !entity.IsActive;
            model.id = entity.Id;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.modifiedBy = entity.UpdatedBy;
            model.modifiedDate = entity.UpdatedDate;
            return model;
        }

        public static DepartmentMasterModel ToModel(this DepartmentMaster entity)
        {            
            var model = ToModel(entity, new DepartmentMasterModel());
            return model;
        }

        public static DepartmentMaster ToEntity(this DepartmentMasterModel model, DepartmentMaster entity)
        {
            entity.Name = model.name;
            entity.Description = model.description;
            entity.Id = model.id ?? 0;
            entity.IsActive = !model.isDeleted;
            entity.CreatedBy =model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.UpdatedBy = model.modifiedBy;
            entity.UpdatedDate = model.modifiedDate;
            return entity;
        }
    }
}
