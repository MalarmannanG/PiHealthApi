using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.VitalsReport
{
    public class VitalsReportModel
    {
        public long id { get; set; }
        public long? appointmentID { get; set; }
        public long? patientID { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public double bp { get; set; }
        public double pulse { get; set; }
        public string bloodPresure { get; set; }
        public double temprature { get; set; }
        public double spO2 { get; set; }
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public long createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public long? updatedBy { get; set; }
        public List<PatientFilesModel> patientFiles { get; set; }
        public List<PatientNewFilesModel> patientNewFiles { get; set; }
    }

    public class PatientFilesModel
    {
        public PatientFilesModel()
        {

        }
        public long id { get; set; }
        public long? appointmentID { get; set; }
        public long? patientID { get; set; }
        public string filePath { get; set; }
        public string fileName { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public long createdBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public long? updatedBy { get; set; }
    }

    public class PatientNewFilesModel
    {
        public string fileName { get; set; }
        public string base64String { get; set; }
    }

    public class GetFileInput
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
    }
}
