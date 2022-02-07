using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class Patient : BaseEntity
    {
        public Patient()
        {
            Appointments = new List<Appointment>();
            VitalsReports = new List<VitalsReport>();
            PatientFiles = new List<PatientFiles>();
            PatientProfile = new List<PatientProfile>();
        }
        public string PatientName { get; set; }
        public string Initial { get; set; }
        public string HrNo { get; set; }
        public string AttendarName { get; set; }
        public string PatientGender { get; set; }
        public int Gender { get; set; }
        public bool IsDeleted { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? DOB { get; set; }
        public float Age { get; set; }        
        public string Address { get; set; }       
        public long? DoctorMasterId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public string UlID { get; set; }
        public DoctorMaster DoctorMaster { get; set; }
        public virtual ICollection<PatientProfile> PatientProfile { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<VitalsReport> VitalsReports { get; set; }
        public virtual ICollection<PatientFiles> PatientFiles { get; set; }

    }
}
