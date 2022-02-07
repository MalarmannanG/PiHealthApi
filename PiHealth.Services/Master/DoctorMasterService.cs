using PiHealth.DataModel;
using PiHealth.DataModel.Entity;
using PiHealth.DataModel.Entity.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHealth.Services.Master
{   
    public class DoctorMasterService
    {
        public readonly IRepository<DoctorMaster> _repository;     
        public DoctorMasterService(IRepository<DoctorMaster> repository)
        {
            _repository = repository;           
        }      

        public virtual IQueryable<DoctorMaster> GetAll(string name = null)
        {
            var data = _repository.Table.Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(a => a.Name.Contains(name));
            }

            return data;
        }
        public virtual IQueryable<DoctorMaster> AutoComplete(string name = null)
        {
            var data = _repository.Table.Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                data = data.Where(a => a.Name.Contains(name));
            }

            return data;
        }

        public virtual async Task<DoctorMaster> Update(DoctorMaster entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public virtual async Task<DoctorMaster> Create(DoctorMaster entity)
        {
           return await _repository.InsertAsync(entity);
        }

        public virtual async Task Delete(DoctorMaster entity)
        {
            entity.IsDeleted = true;
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task<DoctorMaster> Get(long id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
