using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.Web.Model.Patient;

namespace PiHealth.Web.Model.DoctorMaster
{
    public class DoctorMasterModel : BaseResponseModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public string qualification { get; set; }
        public string clinicName { get; set; }
        public string notes { get; set; }
        public string gender { get; set; }
        public string department { get; set; }
        public string address { get; set; }
        public string telNo { get; set; }
        public string phoneNo1 { get; set; }
        public string phoneNo2 { get; set; }
        public string email { get; set; }
        public bool isDeleted { get; set; }
        public double percentage { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public long createdBy { get; set; }
        public long? modifiedBy { get; set; }
    }

    public class DoctorQueryModel:BaseQueryModel
    {
        public string name { get; set; }
    }

}
