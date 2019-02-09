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
        public string iD { get; set; }
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

        public DateTime dOB { get; set; }
        public Customer(string iD, string name, string surname, string street, string city, string province, string country, string postcode, string mobileNum, string homeNum, string email, DateTime dOB)
        {
            this.iD = iD;
            this.name = name;
            this.surname = surname;
            this.street = street;
            this.city = city;
            this.province = province;
            this.country = country;
            this.postcode = postcode;
            this.mobileNum = mobileNum;
            this.homeNum = homeNum;
            this.email = email;
            this.dOB = dOB;
        }


    }
}
