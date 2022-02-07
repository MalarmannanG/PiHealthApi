using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.PrescriptionMaster
{
    public class PrescriptionMasterModel
    {
        public long id { get; set; }
        public string categoryName { get; set; }
        public string genericName { get; set; }
        public string medicinName { get; set; }
        public string strength { get; set; }
        public string units { get; set; }
        public string description { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public long createdBy { get; set; }
        public long? modifiedBy { get; set; }
    }

    public class PrescriptionMasterQueryModel
    {
        public int take { get; set; }
        public int skip { get; set; }
        public string name { get; set; }
    }
}
