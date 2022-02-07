using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class PatientFiles : BaseEntity
    {
        public PatientFiles()
        {

        }

        public long? AppointmentID { get; set; }
        public long? PatientID { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public Appointment Appointment { get; set; }
        public Patient Patient { get; set; }
    }
}
