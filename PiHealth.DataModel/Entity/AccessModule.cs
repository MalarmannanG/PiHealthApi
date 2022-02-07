using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class AccessModule : BaseEntity
    {
        public AccessModule()
        {
            this.AccessFunctions = new List<AccessFunction>();
        }
        public string ModuleCode { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AccessFunction> AccessFunctions { get; set; }

    }
}
