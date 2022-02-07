using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.Web.Model.Patient;

namespace PiHealth.Web.Model
{
    public class ResultViewModel
    {
        public long id { get; set; }
        public string reportNo { get; set; }
        public string ulId { get; set; }
        public string patientName { get; set; }
        public string mobileNo { get; set; }
        public int? gender { get; set; }        
        public float? age { get; set; }
        public string address { get; set; }
        public string referredBy { get; set; }
        public long? referredById { get; set; }
        public int? status { get; set; }
        public DateTime? collectedDate { get; set; }
        public DateTime? verfiedDate { get; set; }
    }

    public class ResultEntryModel
    {
        public ResultEntryModel()
        {
        }

        public long id { get; set; }
        public string ulId { get; set; }
        public DateTime? dob { get; set; }
        public string patientName { get; set; }
        public string mobileNo { get; set; }
        public int? gender { get; set; }
        public float? age { get; set; }
        public string address { get; set; }
        public string referredBy { get; set; }
        public long? referredById { get; set; }
        public int? status { get; set; }        
        public string barcode { get; set; }        
        public DateTime? collectedDate { get; set; }        
        public DateTime? verfiedDate { get; set; }
    }
}
