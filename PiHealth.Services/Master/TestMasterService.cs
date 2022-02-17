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
    public class TestMasterService
    {
        public readonly IRepository<TestMaster> _repository;     
        public TestMasterService(IRepository<TestMaster> repository)
        {
            _repository = repository;           
        }      

        public virtual IQueryable<TestMaster> GetAll(string name = null)
        {
            var data = _repository.Table.Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(name))
            {
                //data = data.Where(a => a.Name.Contains(name));
                data = data.WhereIf(!string.IsNullOrWhiteSpace(name), e => false || e.Name.Contains(name) || e.Department.Contains(name) || e.Remarks.Contains(name));
            }

            return data;
        }
        public virtual IQueryable<TestMaster> AutoComplete(string name = null)
        {
            var data = _repository.Table.Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                data = data.Where(a => a.Name.Contains(name));
            }

            return data;
        }

        public virtual async Task<TestMaster> Update(TestMaster entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public virtual async Task<TestMaster> Create(TestMaster entity)
        {
           return await _repository.InsertAsync(entity);
        }

        public virtual async Task Delete(TestMaster entity)
        {
            entity.IsDeleted = true;
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task<TestMaster> Get(long id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
