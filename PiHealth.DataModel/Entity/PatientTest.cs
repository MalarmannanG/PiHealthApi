using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class PatientTest : BaseEntity
    {
        public PatientTest()
        {
        }

        public long PatientProfileId { get; set; }
        public long TestMasterId { get; set; }
        public string Remarks { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public virtual TestMaster TestMaster { get; set; }
        public virtual PatientProfile PatientProfile { get; set; }

    }
}
