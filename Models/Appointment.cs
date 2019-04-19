using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Appointment
    {
        public int ID { get; set; }
        public string PatientID { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
    }
}
