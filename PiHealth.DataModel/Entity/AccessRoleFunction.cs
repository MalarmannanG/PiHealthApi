using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class AccessRoleFunction : BaseEntity
    {
        public string Role { get; set; }
        public long FunctionID { get; set; }
        public virtual AccessFunction AccessFunctions { get; set; }        
    }
}
