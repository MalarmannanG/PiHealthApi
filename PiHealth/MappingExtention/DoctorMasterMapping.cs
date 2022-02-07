using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.DataModel.Entity;
using PiHealth.Web.Model.Account;
using PiHealth.Web.Model.DoctorMaster;

namespace PiHealth.Web.MappingExtention
{
    public static class DoctorMasterMapping
    {
        public static DoctorMaster ToEntity(this DoctorMasterModel model, DoctorMaster entity)
        {
            entity.Id = model.id;
            entity.Address = model.address;
            entity.ClinicName = model.clinicName;
            entity.CreatedBy = model.createdBy;
            entity.Gender = model.gender;
            entity.CreatedDate = model.createdDate;
            entity.Department = model.department;
            entity.Email = model.email;
            entity.IsDeleted = model.isDeleted;
            entity.ModifiedBy = model.modifiedBy;
            entity.ModifiedDate = model.modifiedDate;
            entity.Name = model.name;
            entity.Notes = model.notes;
            entity.Percentage = model.percentage;
            entity.PhoneNo1 = model.phoneNo1;
            entity.PhoneNo2 = model.phoneNo2;
            entity.Qualification = model.qualification;
            entity.TelNo = model.telNo;

            return entity;
        }

        public static DoctorMasterModel ToModel(this DoctorMaster entity, DoctorMasterModel model)
        {
            model.id = entity.Id;
            model.address = entity.Address;
            model.clinicName = entity.ClinicName;
            model.createdBy = entity.CreatedBy;
            model.gender = entity.Gender;
            model.department = entity.Department;
            model.createdDate = entity.CreatedDate;
            model.email = entity.Email;
            model.isDeleted = entity.IsDeleted;
            model.modifiedBy = entity.ModifiedBy;
            model.modifiedDate = entity.ModifiedDate;
            model.name = entity.Name;
            model.notes = entity.Notes;
            model.percentage = entity.Percentage;
            model.phoneNo1 = entity.PhoneNo1;
            model.phoneNo2 = entity.PhoneNo2;
            model.qualification = entity.Qualification;
            model.telNo = entity.TelNo;

            return model;
        }
    }
}
