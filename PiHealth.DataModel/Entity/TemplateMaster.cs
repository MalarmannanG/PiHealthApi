using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class TemplateMaster : BaseEntity
    {
        public TemplateMaster()
        {
            TemplatePrescriptions = new List<TemplatePrescription>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Compliants { get; set; }
        public string Examination { get; set; }
        public string Impression { get; set; }
        public string Advice { get; set; }
        public string Plan { get; set; }
        public string FollowUp { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public List<PatientProfile> PatientProfile { get; set; }
        public virtual ICollection<TemplatePrescription> TemplatePrescriptions { get; set; }

    }

}
