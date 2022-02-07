using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class Prescription : BaseEntity
    {
        public Prescription()
        {
        }

        public long PatientProfileId { get; set; }
        public string MedicinName { get; set; }
        public string GenericName { get; set; }
        public string Strength { get; set; }
        public bool BeforeFood { get; set; }
        public bool Morning { get; set; }
        public bool Noon { get; set; }
        public bool Night { get; set; }
        public string Description { get; set; }
        public int NoOfDays { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual PatientProfile PatientProfile { get; set; }

    }
}
