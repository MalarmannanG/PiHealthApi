using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.Patient
{
    public class PatientModel
    {
        public PatientModel()
        {

        }

        public long id { get; set; }
        public long? referedBy { get; set; }
        public double? actualCost { get; set; }
        public string referedByName { get; set; }
        public string description { get; set; }
        public string patientName { get; set; }
        public DateTime? dob { get; set; }
        public bool isDeleted { get; set; }
        public string initial { get; set; }
        public string gender { get; set; }
        public string mobileNumber { get; set; }
        public string hrno { get; set; }
        public string attendarName { get; set; }
        public float? age { get; set; }
        public string address { get; set; }
        public string ulId { get; set; }
        public long createdBy { get; set; }
        public DateTime? createdOn { get; set; }
        public long modifiedBy { get; set; }
        public DateTime? modifiedOn { get; set; }
    }

    public class PatientQueryModel:BaseQueryModel
    {
        public string name { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public bool isTodayPatients { get; set; }
    }

}
