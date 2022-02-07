using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.ResponseModel
{
    public class LoginResponseModel : BaseResponseModel
    {
        public string AccessToken { get; set; }
    }
   
}
