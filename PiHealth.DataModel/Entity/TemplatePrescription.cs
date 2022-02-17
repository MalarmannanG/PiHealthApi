using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class TemplatePrescription : BaseEntity
    {
        public TemplatePrescription()
        {

        }

        public long TemplateMasterId { get; set; }
        public string MedicineName { get; set; }
        public string Strength { get; set; }
        public bool BeforeFood { get; set; }
        public bool Morning { get; set; }
        public bool Noon { get; set; }
        public bool Night { get; set; }
        public string Remarks { get; set; }
        public int NoOfDays { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual TemplateMaster TemplateMaster { get; set; }
    }
}
