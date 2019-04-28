using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Models
{
   public  class AppointmentUserEmail
    {
        public int ID { get; set; }
        public string PatientID { get; set; }
        public string Date { get; set; }
        public AppointmentStatus Status { get; set; }
        public string email { get; set; }

        public AppointmentUserEmail()
        {

        }

        public AppointmentUserEmail(int iD, string patientID, DateTime date, AppointmentStatus status, string email)
        {
            ID = iD;
            PatientID = patientID;
            Date = date.ToString("yyyy-MM-ddTHH:mm:ssZ");
            Status = status;
            this.email = email;
        }
        public AppointmentUserEmail(Appointment appointment,string email)
        {
            ID = appointment.ID;
            PatientID = appointment.PatientID;
            Date = appointment.Date.ToString("yyyy-MM-ddTHH:mm:ssZ");
            Status = appointment.Status;
            this.email = email;
            Debug.WriteLine(Date);
        }
    }
}
