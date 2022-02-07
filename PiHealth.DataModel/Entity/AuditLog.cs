using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class AuditLog : BaseEntity
    {
        public string UserID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }
        public string IP { get; set; }
        public string UserAgent { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}
