using PiHealth.DataModel;
using PiHealth.DataModel.Entity;
using PiHealth.DataModel.Entity.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PiHealth.Services.Master
{   
    public class AppointmentService
    {
        public readonly IRepository<Appointment> _repository;     
        public AppointmentService(IRepository<Appointment> repository)
        {
            _repository = repository;           
        }      

        public virtual IQueryable<Appointment> GetAll(long[] patientIds = null, long[] doctorIds = null, bool? isProcedure = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var data = _repository.Table.Where(a => a.IsActive).Include(a => a.Patient).ThenInclude(a => a.DoctorMaster).AsQueryable();

            if(patientIds?.Count() > 0)
            {
                data = data.Where(a => a.PatientId != null && patientIds.Contains(a.PatientId.Value));
            }

            if (doctorIds?.Count() > 0)
            {
                data = data.Where(a => a.AppUserId != null && doctorIds.Contains(a.AppUserId.Value));
            }

            if (isProcedure != null)
            {
                if (isProcedure == true)
                {
                    data = data.Where(a => a.VisitType == "Procedure");
                } else
                {
                    data = data.Where(a => a.VisitType != "Procedure");
                }
            }

            if (fromDate != null)
            {
                data = data.Where(a => a.AppointmentDateTime.Date >= fromDate.Value.Date);
            }


            if (toDate != null)
            {
                data = data.Where(a => a.AppointmentDateTime.Date <= toDate.Value.Date);
            }

            return data.Include(a => a.AppUser);
        }

        public virtual async Task<Appointment> Update(Appointment entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public virtual async Task<Appointment> Create(Appointment entity)
        {
           return await _repository.InsertAsync(entity);
        }

        public virtual async Task Delete(Appointment entity)
        {
            entity.IsActive = false;
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task<Appointment> Get(long id)
        {
            return await _repository.Table.Where(a => a.Id == id)?.Include(a => a.VitalsReport).Include(a => a.AppUser).Include(a=> a.PatientFiles).Include(a => a.Patient).ThenInclude(a => a.DoctorMaster)?.FirstOrDefaultAsync();
        }
    }
}
