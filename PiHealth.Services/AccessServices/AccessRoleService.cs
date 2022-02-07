using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiHealth.DataModel;
using PiHealth.DataModel.Entity;

namespace PiHealth.Services.AccessServices
{
    public class AccessRoleFunctionService
    {
        public readonly IRepository<AccessRoleFunction> _repository;

        public AccessRoleFunctionService(IRepository<AccessRoleFunction> repository)
        {
            _repository = repository;
        }

        public virtual IQueryable<AccessRoleFunction> GetAll(long? functionId = null, string role = null)
        {
            var data = _repository.TableNoTracking.Include(a => a.AccessFunctions).AsQueryable();

            if (functionId != null)
            {
                data = data.Where(a => a.FunctionID == functionId);
            }
            if (!string.IsNullOrEmpty(role))
            {
                data = data.Where(a => a.Role.ToLower() == role.ToLower());
            }

            return data;
        }

        public virtual AccessRoleFunction Update(AccessRoleFunction entity)
        {
            return _repository.Update(entity);
        }

        public virtual AccessRoleFunction Add(AccessRoleFunction entity)
        {
            return _repository.Insert(entity);
        }

        public virtual Task Create(IEnumerable<AccessRoleFunction> sysRoleFunction)
        {
            return _repository.InsertAsync(sysRoleFunction);
        }

        public AccessRoleFunction Get(long id)
        {
            return _repository.Table.Where(a => a.Id == id).Include(a => a.AccessFunctions).FirstOrDefault();
        }

        public virtual void Delete(AccessRoleFunction entity)
        {
            _repository.Delete(entity);
        }

    }
}
