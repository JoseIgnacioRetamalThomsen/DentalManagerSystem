using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Models
{
    public class Treatment 
    {
        public string iD { get; set; }


        public string name { get; set; }
        public decimal price { get; set; }


        

        public Treatment(string iD, string name, decimal price)
        {
            this.iD = iD;
            this.name = name;
            this.price = price;
        }


        public override string ToString()
        {
            return this.name;
        }

       

      

    }
}
