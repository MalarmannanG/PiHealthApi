using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class VitalsReport : BaseEntity
    {
        public VitalsReport()
        {
        }

        public long? AppointmentID { get; set; }
        public long? PatientID { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string BloodPresure { get; set; }
        public double BP { get; set; }
        public double Pulse { get; set; }
        public double Temprature { get; set; }
        public double SpO2 { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public Appointment Appointment { get; set; }
        public Patient Patient { get; set; }

    }
}
