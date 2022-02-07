using PiHealth.DataModel;
using PiHealth.DataModel.Entity;
using PiHealth.DataModel.Entity.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PiHealth.Services.Master
{
    public class DepartmentService
    {
        public readonly IRepository<DepartmentMaster> _repository;     
        public DepartmentService(IRepository<DepartmentMaster> repository)
        {
            _repository = repository;           
        }      

        public virtual IQueryable<DepartmentMaster> GetAll(bool? isActive = null, string name = null)
        {
            var data = _repository.Table;

            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(a => a.Name.Contains(name));
            }

            if(isActive != null)
            {
                data = data.Where(a => a.IsActive== isActive.Value);
            }

            return data;
        }

        public virtual DepartmentMaster Update(DepartmentMaster entity)
        {
            return _repository.Update(entity);
        }

        public virtual DepartmentMaster Add(DepartmentMaster entity)
        {
            return _repository.Insert(entity);
        }
        public virtual IQueryable<DepartmentMaster> GetActive()
        {
           return GetAll(isActive: true);
        }

        public DepartmentMaster Get(long id)
        {
            return _repository.Table.Where(a => a.Id == id).FirstOrDefault();
        }
    }
}
