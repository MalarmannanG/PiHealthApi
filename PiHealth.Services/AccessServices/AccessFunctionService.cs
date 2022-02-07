using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PiHealth.DataModel;
using PiHealth.DataModel.Entity;

namespace PiHealth.Services.AccessServices
{
    public class AccessFunctionService
    {
        public readonly IRepository<AccessFunction> _repository;
        public AccessFunctionService(IRepository<AccessFunction> repository)
        {
            _repository = repository;
        }

        public virtual IQueryable<AccessFunction> GetAll()
        {
            var data = _repository.TableNoTracking.AsQueryable();

            return data;
        }

        public virtual AccessFunction Update(AccessFunction entity)
        {
            return _repository.Update(entity);
        }

        public virtual AccessFunction Add(AccessFunction entity)
        {
            return _repository.Insert(entity);
        }

        public AccessFunction Get(long id)
        {
            return _repository.Table.Where(a => a.Id == id).Include(a => a.AccessRole).FirstOrDefault();
        }
    }
}
