using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.DataModel.Entity;
using PiHealth.Web.Model;

namespace PiHealth.Web.MappingExtention
{
    public static class UserMapping
    {
        public static UserModel ToModel(this AppUser entity, UserModel model)
        {
            model.id = entity.Id;
            model.email = entity.Email;
            model.name = entity.Name;
            model.userName = entity.Username;
            model.userType = entity.UserType;
            model.serialNo = entity.SerialNumber;
            model.phoneNo = entity.PhoneNo;            
            model.isActive = entity.IsActive;
            model.gender = entity.Gender;
            model.address = entity.Address;
            model.lastLoggedIn = entity.LastLoggedIn;
            return model;
        }

        public static UserModel ToModel(this AppUser entity)
        {
            var model = new UserModel();
            model.id = entity.Id;
            model.email = entity.Email;
            model.name = entity.Name;
            model.userName = entity.Username;
            model.userType = entity.UserType;
            model.serialNo = entity.SerialNumber;
            model.phoneNo = entity.PhoneNo;
            model.isActive = entity.IsActive;
            model.gender = entity.Gender;
            model.address = entity.Address;
            model.lastLoggedIn = entity.LastLoggedIn;
            return model;
        }

        public static AppUser ToEntity(this UserModel model, AppUser entity)
        {
            entity.Id = model.id;
            entity.Email = model.email;
            entity.Name = model.name;
            entity.Username = model.userName;
            entity.UserType = model.userType;
            entity.SerialNumber = model.serialNo;
            entity.PhoneNo = model.phoneNo;
            entity.IsActive = model.isActive;
            entity.LastLoggedIn = model.lastLoggedIn;
            entity.Gender = model.gender;
            entity.Address = model.address;
            return entity;
        }
    }
}
