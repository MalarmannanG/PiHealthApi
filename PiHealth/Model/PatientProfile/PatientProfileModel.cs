using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiHealth.Web.Model.Appointment;
using PiHealth.Web.Model.Patient;

namespace PiHealth.Web.Model.PatientProfile
{
    public class PatientProfileModel
    {
        public PatientProfileModel()
        {
            prescriptionModel = new List<PrescriptionModel>();
        }
        public long id { get; set; }
        public long? templateMasterId { get; set; }
        public long patientId { get; set; }
        public long doctorId { get; set; }
        public long appointmentId { get; set; }
        public string compliants { get; set; }
        public string examination { get; set; }
        public string impression { get; set; }
        public string advice { get; set; }
        public string plan { get; set; }
        public string followUp { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public long createdBy { get; set; }
        public long? modifiedBy { get; set; }
        public PatientModel patientModel { get; set; }
        public AppointmentModel appointment { get; set; }
        public List<PatientDiagnosisModel> patientDiagnosisModel { get; set; }
        public List<PrescriptionModel> prescriptionModel { get; set; }
        public List<PatientTestModel> patientTestModel { get; set; }
    }
    public class PatientProfileQueryModel
    {
        public long? PatientId { get; set; }
        public DateTime? appointmentDate { get; set; }
    }

    public class PatientTestModel
    {
        public PatientTestModel()
        {
        }
        public long id { get; set; }
        public long patientProfileId { get; set; }
        public long testMasterId { get; set; }
        public string testMasterName { get; set; }
        public string remarks { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public long createdBy { get; set; }
        public long? modifiedBy { get; set; }
    }



    public class PatientDiagnosisModel
    {
        public PatientDiagnosisModel()
        {

        }

        public long id { get; set; }
        public long patientProfileId { get; set; }
        public long diagnosisMasterId { get; set; }
        public string diagnosisMasterName { get; set; }
        public string description { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public long createdBy { get; set; }
        public long? modifiedBy { get; set; }

    }

    public class PrescriptionModel
    {
        public PrescriptionModel()
        {

        }

        public long id { get; set; }
        public long patientProfileId { get; set; }
        public string medicinName { get; set; }
        public string genericName { get; set; }
        public string strength { get; set; }
        public bool beforeFood { get; set; }
        public bool morning { get; set; }
        public bool noon { get; set; }
        public bool night { get; set; }
        public string description { get; set; }
        public int noOfDays { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public long createdBy { get; set; }
        public long? modifiedBy { get; set; }
    }
}
