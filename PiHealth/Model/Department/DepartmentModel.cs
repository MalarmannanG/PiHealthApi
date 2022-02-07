using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.Department
{   
    public class DepartmentMasterModel
    {
        public long? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public long createdBy { get; set; }
        public long? modifiedBy { get; set; }

    }
}
