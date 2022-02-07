using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class PatientProfile : BaseEntity
    {
        public PatientProfile()
        {
            Prescriptions = new List<Prescription>();
            PatientTests = new List<PatientTest>();
            PatientDiagnosis = new List<PatientDiagnosis>();            
        }

        public long PatientId { get; set; }
        public long DoctorId { get; set; }
        public long? TemplateMasterId { get; set; }
        public long AppointmentId { get; set; }
        public string Compliants { get; set; }
        public string Examination { get; set; }
        public string Impression { get; set; }
        public string Advice { get; set; }
        public string Plan { get; set; }
        public string FollowUp { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public virtual TemplateMaster TemplateMaster { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual ICollection<PatientDiagnosis> PatientDiagnosis { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
        public virtual ICollection<PatientTest> PatientTests { get; set; }        

    }
}
