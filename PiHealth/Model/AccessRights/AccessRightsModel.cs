using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.AccessRights
{
    public class AccessRightsModel
    {
        public AccessRightsModel()
        {
            modules = new List<ModuleDetails>();
            roles = new List<RoleDetails>();
        }
        public string role { get; set; }
        public List<RoleDetails> roles { get; set; }
        public List<ModuleDetails> modules { get; set; }
    }

    public class RoleDetails
    {
        public string name { get; set; }
        public string value { get; set; }

    }

    public class RoleFunctionsDetailsModel
    {
        public string role { get; set; }
        public List<int> functionIds { get; set; }
    }

    public class ModuleDetails
    {
        public ModuleDetails()
        {
            functions = new List<FunctionDetails>();
        }

        public long id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public List<FunctionDetails> functions { get; set; }
    }

    public class FunctionDetails
    {
        public long id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public bool haveAccess { get; set; }
    }
}
