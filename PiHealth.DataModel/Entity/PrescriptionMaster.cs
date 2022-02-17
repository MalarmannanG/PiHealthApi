using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class PrescriptionMaster : BaseEntity
    {
        public PrescriptionMaster()
        {

        }

        public string GenericName { get; set; }
        public string CategoryName { get; set; }
        public string MedicineName { get; set; }
        public string Strength { get; set; }
        public string Units { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
