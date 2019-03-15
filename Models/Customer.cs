///------------------------------------------
///
///  Dental Manager System
///  Profesional Practice in IT project
///  GMIT 2019
///  
///  Markm Ndpenoch
///  Jose I. Retamal
///------------------------------------------
///

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Models
{
    public class Customer
    {
        private string v;

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
        public string comments { get; set; }

        public Customer(string iD, string name, string surname, string street, string city, string province, string country, string postcode, string mobileNum, string homeNum, string email, DateTime dOB, string comments)
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
            this.comments = comments;
        }

        public Customer(string v)
        {
            this.v = v;
        }

        public void Print()
        {
            Debug.WriteLine("id" + this.iD);
            Debug.WriteLine("name" + this.name);
            Debug.WriteLine("surname" + this.surname);
            Debug.WriteLine("street" + this.street);
            Debug.WriteLine("city" + this.city);
            Debug.WriteLine("province" + this.province);
            Debug.WriteLine("country" + this.country);
            Debug.WriteLine("postcode" + this.postcode);
            Debug.WriteLine("mobilNum" + this.mobileNum);
            Debug.WriteLine("homeNum" + this.homeNum);
            Debug.WriteLine("email" + this.email);
            Debug.WriteLine("dob" + this.dOB);
            Debug.WriteLine("comment" + this.comments);

        }

    }
}
