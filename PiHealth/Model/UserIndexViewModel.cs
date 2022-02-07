using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model
{
    public class UserModel
    {
        public long id { get; set; }
        public string email { get; set; }
        public bool isActive { get; set; }
        public DateTimeOffset? lastLoggedIn { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string phoneNo { get; set; }
        public string serialNo { get; set; }
        public string userType { get; set; }
        public string userName { get; set; }
        public DateTime? createdDate { get; set; }
    }

    public class UserQueryModel : BaseQueryModel
    {
        public string name { get; set; }
    }
}
