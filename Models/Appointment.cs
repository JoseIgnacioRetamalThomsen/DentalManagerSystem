using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Appointment
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
    }
}
