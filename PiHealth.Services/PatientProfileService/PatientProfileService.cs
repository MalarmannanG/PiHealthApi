using PiHealth.DataModel;
using PiHealth.DataModel.Entity;
using PiHealth.DataModel.Entity.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PiHealth.Services.PatientProfileService
{   
    public class PatientProfileService
    {
        public readonly IRepository<PatientProfile> _repository;     
        public PatientProfileService(IRepository<PatientProfile> repository)
        {
            _repository = repository;           
        }      

        public virtual IQueryable<PatientProfile> GetAll(string name = null, long? patientId = null, long[] appointmentIds = null)
        {
            var data = _repository.Table.Where(a => !a.IsDeleted).Include(a => a.Appointment).Include(a=> a.Prescriptions).AsQueryable();

            if(patientId != null)
            {
                data = data.Where(a => a.PatientId == patientId);
            }

            if (appointmentIds != null && appointmentIds.Count() > 0)
            {
                data = data.Where(a => appointmentIds.Contains(a.AppointmentId));
            }


            return data;
        }

        public virtual IQueryable<PatientProfile> AutoComplete(string name = null)
        {
            var data = _repository.Table.Where(a => !a.IsDeleted);

            
            return data;
        }

        public virtual async Task<PatientProfile> Update(PatientProfile entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public virtual async Task<PatientProfile> Create(PatientProfile entity)
        {
           return await _repository.InsertAsync(entity);
        }

        public virtual async Task Delete(PatientProfile entity)
        {
            entity.IsDeleted = true;
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task<PatientProfile> Get(long id)
        {
            return await _repository.Table.Where(a => a.AppointmentId == id).Include(a => a.Patient).Include(a => a.PatientDiagnosis).ThenInclude(a => a.DiagnosisMaster).Include(a => a.PatientTests).ThenInclude(a => a.TestMaster).Include(a => a.Prescriptions).Include(a => a.Appointment).ThenInclude(a => a.AppUser).Include(a=>a.Appointment.PatientFiles).Include(a => a.Appointment.VitalsReport).FirstOrDefaultAsync();
        }
    }
}
