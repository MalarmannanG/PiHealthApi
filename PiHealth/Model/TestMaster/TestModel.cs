using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.TestMaster
{
    public class TestMasterModel
    {
        public long? id { get; set; }
        public string department { get; set; }
        public string name { get; set; }
        public string remarks { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public bool isDeleted { get; set; }
        public long createdBy { get; set; }
        public long? modifiedBy { get; set; }

    }

    public class TestMasterQueryModel
    {
        public string name { get; set; }
        public string orderBy { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
    }
}
