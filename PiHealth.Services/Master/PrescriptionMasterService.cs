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
    public class PrescriptionMasterService
    {
        public readonly IRepository<PrescriptionMaster> _repository;
        public PrescriptionMasterService(IRepository<PrescriptionMaster> repository)
        {
            _repository = repository;
        }

        public virtual IQueryable<PrescriptionMaster> GetAll(string name = null)
        {
            var data = _repository.Table.Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(a => a.MedicinName.Contains(name) || a.GenericName.Contains(name));
            }

            return data;
        }

        public virtual async Task<PrescriptionMaster> Update(PrescriptionMaster entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public virtual async Task<PrescriptionMaster> Create(PrescriptionMaster entity)
        {
            return await _repository.InsertAsync(entity);
        }

        public virtual async Task Delete(PrescriptionMaster entity)
        {
            entity.IsDeleted = true;
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task<PrescriptionMaster> Get(long id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
