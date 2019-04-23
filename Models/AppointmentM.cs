using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class AppointmentM
    {
        public int Id { get; set; }
        public string _id { get; set; }
        public string PatientID { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; }
        public int __v { get; set; }
       

        public AppointmentM() { }

      
    }
}
