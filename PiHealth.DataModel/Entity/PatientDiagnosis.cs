using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class PatientDiagnosis : BaseEntity
    {
        public PatientDiagnosis()
        {            
        }

        public long PatientProfileId { get; set; }
        public long DiagnosisMasterId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public virtual DiagnosisMaster DiagnosisMaster { get; set; }
        public virtual PatientProfile PatientProfile { get; set; }

    }
}
