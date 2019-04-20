using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public enum AppointmentStatus
    {
        Active,Cancel,Done
    }
    public class Appointment
    {
        public int ID { get; set; }
        public string PatientID { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; }

        public Appointment() { }

        public Appointment(int iD, string patientID, DateTime date, AppointmentStatus status)
        {
            ID = iD;
            PatientID = patientID;
            Date = date;
            Status = status;
        }
    }
}
