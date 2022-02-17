using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class DoctorMaster:BaseEntity
    {
        public DoctorMaster()
        {
            Patients = new List<Patient>();
        }

        public string Name { get; set; }
        public string Qualification { get; set; }
        public string ClinicName { get; set; }
        public string Notes { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string TelNo { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public double Percentage { get; set; }                
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long PinCode { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }

}
