using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.Account
{
    public class LabSettingsModel : BaseResponseModel
    {
        public long id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }

}
