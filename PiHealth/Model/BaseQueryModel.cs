using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model
{
    public class BaseQueryModel
    {
        public int skip { get; set; }
        public int take { get; set; }
        public string order_by { get; set; }
    }
}
