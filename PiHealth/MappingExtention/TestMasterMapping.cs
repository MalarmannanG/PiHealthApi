using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.DataModel.Entity;
using PiHealth.Web.Model.TestMaster;

namespace PiHealth.Web.MappingExtention
{
    public static class TestMasterMapping
    {
        public static TestMasterModel ToModel(this TestMaster entity, TestMasterModel model)
        {
            model.name = entity.Name;
            model.department = entity.Department;
            model.remarks = entity.Remarks;
            model.isDeleted = entity.IsDeleted;
            model.id = entity.Id;
            model.createdBy = entity.CreatedBy;
            model.createdDate = entity.CreatedDate;
            model.modifiedBy = entity.ModifiedBy;
            model.modifiedDate = entity.ModifiedDate;
            return model;
        }

        public static TestMaster ToEntity(this TestMasterModel model, TestMaster entity)
        {
            entity.Name = model.name;
            entity.Department = model.department;
            entity.Remarks = model.remarks;
            entity.Id = model.id ?? 0;
            entity.IsDeleted = model.isDeleted;
            entity.CreatedBy =model.createdBy;
            entity.CreatedDate = model.createdDate;
            entity.ModifiedBy = model.modifiedBy;
            entity.ModifiedDate = model.modifiedDate;
            return entity;
        }
    }
}
