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
    public class DiagnosisMasterService
    {
        public readonly IRepository<DiagnosisMaster> _repository;     
        public DiagnosisMasterService(IRepository<DiagnosisMaster> repository)
        {
            _repository = repository;           
        }      

        public virtual IQueryable<DiagnosisMaster> GetAll(string name = null)
        {
            var data = _repository.Table.Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(name))
            {
                //data = data.Where(a => a.Name.Contains(name));
                data = data.WhereIf(!string.IsNullOrWhiteSpace(name), e => false || e.Name.Contains(name) || e.Description.Contains(name));
            }

            return data;
        }
        public virtual IQueryable<DiagnosisMaster> AutoComplete(string name = null)
        {
            var data = _repository.Table.Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                data = data.Where(a => a.Name.Contains(name));
            }

            return data;
        }

        public virtual async Task<DiagnosisMaster> Update(DiagnosisMaster entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public virtual async Task<DiagnosisMaster> Create(DiagnosisMaster entity)
        {
           return await _repository.InsertAsync(entity);
        }

        public virtual async Task Delete(DiagnosisMaster entity)
        {
            entity.IsDeleted = true;
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task<DiagnosisMaster> Get(long id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
