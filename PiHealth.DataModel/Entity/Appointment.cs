using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class Appointment : BaseEntity
    {
        public Appointment()
        {
            PatientFiles = new List<PatientFiles>();
        }

        public long? PatientId { get; set; }
        public long? AppUserId { get; set; }
        public string Description { get; set; }
        public string VisitType { get; set; }
        public bool IsActive { get; set; }
        public string DayOrNight { get; set; }
        public string TimeOfAppintment { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }

        public VitalsReport VitalsReport { get; set; }
        public virtual ICollection<PatientFiles> PatientFiles { get; set; }
        public PatientProfile PatientProfile { get; set; }
        public Patient Patient { get; set; }
        public AppUser AppUser { get; set; }

    }
}
