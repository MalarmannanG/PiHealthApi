using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.DiagnosisMaster
{
    public class DiagnosisMasterModel
    {
        public long? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createdDate { get; set; }
        public long createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public long? updatedBy { get; set; }
        public bool isDeleted { get; set; }
        
    }

    public class DiagnosisQueryModel
    {
        public string name { get; set; }
        public string orderBy { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
    }
}
