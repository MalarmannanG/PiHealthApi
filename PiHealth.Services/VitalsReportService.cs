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
    public class VitalsReportService
    {
        public readonly IRepository<VitalsReport> _repository;     
        public VitalsReportService(IRepository<VitalsReport> repository)
        {
            _repository = repository;           
        }      

        public virtual IQueryable<VitalsReport> GetAll(bool? isActive = null, string name = null)
        {
            var data = _repository.Table;


            if(isActive != null)
            {
                data = data.Where(a => a.IsActive== isActive.Value);
            }
            data = data.Include(a => a.Appointment).AsQueryable();
            return data;
        }

        public virtual VitalsReport Update(VitalsReport entity)
        {
            return _repository.Update(entity);
        }

        public virtual VitalsReport Add(VitalsReport entity)
        {
            return _repository.Insert(entity);
        }
        public virtual IQueryable<VitalsReport> GetActive()
        {
           return GetAll(isActive: true);
        }

        public VitalsReport GetByAppointmentId(long id)
        {
            return _repository.Table.Where(a => a.AppointmentID == id).Include(a => a.Appointment).ThenInclude(a=> a.PatientFiles).FirstOrDefault();
        }

        public VitalsReport Get(long id)
        {
            return _repository.Table.Where(a => a.Id == id).Include(a => a.Appointment).FirstOrDefault();
        }
    }
}
