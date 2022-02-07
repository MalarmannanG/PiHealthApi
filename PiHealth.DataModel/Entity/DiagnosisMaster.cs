using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class DiagnosisMaster : BaseEntity
    {
        public DiagnosisMaster()
        {
            PatientDiagnosis = new List<PatientDiagnosis>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public PatientProfile PatientProfile { get; set; }
        public virtual ICollection<PatientDiagnosis> PatientDiagnosis { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
