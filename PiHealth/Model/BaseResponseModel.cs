using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model
{
    public class BaseResponseModel
    {
        public double ErrorCode { get; set; } 
        public string ErrorMessage { get; set; }
    }
   
}


