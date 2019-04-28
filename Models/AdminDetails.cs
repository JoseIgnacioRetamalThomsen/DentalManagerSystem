using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class AdminDetails
    {
        public string firstName { get; set; }
        public string surname { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string postcode { get; set; }
        public string mobileNum { get; set; }
        public string homeNum { get; set; }
        public string email { get; set; }

        public AdminDetails(string firstName, string surname, string street, string city, string province, string country, string postcode, string mobileNum, string homeNum, string email)
        {
            this.firstName = firstName;
            this.surname = surname;
            this.street = street;
            this.city = city;
            this.province = province;
            this.country = country;
            this.postcode = postcode;
            this.mobileNum = mobileNum;
            this.homeNum = homeNum;
            this.email = email;
        }

        public AdminDetails()
        {
        }
    }
}
