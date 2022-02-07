using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class TestMaster : BaseEntity
    {
        public TestMaster()
        {
            PatientTests = new List<PatientTest>();
        }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public virtual ICollection<PatientTest> PatientTests { get; set; }

    }
}
