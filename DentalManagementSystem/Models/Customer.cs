using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Models
{
    /// <summary>
    /// All data about customer.
    /// </summary>
    public class Customer
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string postcode { get; set; }
        public string mobileNum { get; set; }
        public string homeNum { get; set; }
        public string email { get; set; }

    }
}
