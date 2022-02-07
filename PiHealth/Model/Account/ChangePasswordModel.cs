using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.Account
{
    public class ChangePasswordModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
