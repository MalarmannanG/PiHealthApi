using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class AccessFunction : BaseEntity
    {
        public AccessFunction()
        {
            this.AccessRole = new List<AccessRoleFunction>();
        }
        public string FuctionCode { get; set; }
        public long ModuleID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AccessRoleFunction> AccessRole { get; set; }
        public virtual AccessModule AccessModule { get; set; }
    }
}
