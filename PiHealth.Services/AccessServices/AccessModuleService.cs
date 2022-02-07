using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PiHealth.DataModel;
using PiHealth.DataModel.Entity;

namespace PiHealth.Services.AccessServices
{
    public class AccessModuleService
    {
        public readonly IRepository<AccessModule> _repository;
        public AccessModuleService(IRepository<AccessModule> repository)
        {
            _repository = repository;
        }

        public virtual IQueryable<AccessModule> GetAll()
        {
            var data = _repository.TableNoTracking.AsQueryable();

            return data;
        }

        public virtual AccessModule Update(AccessModule entity)
        {
            return _repository.Update(entity);
        }

        public virtual AccessModule Add(AccessModule entity)
        {
            return _repository.Insert(entity);
        }

        public AccessModule Get(long id)
        {
            return _repository.Table.Where(a => a.Id == id).Include(a => a.AccessFunctions).FirstOrDefault();
        }
    }
}
