using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Treatment
    {
        public string iD { get; set; }
        public string name { get; set; }
        public Decimal price { get; set; }

        public Treatment(string iD, string name, Decimal price)
        {
            this.iD = iD;
            this.name = name;
            this.price = price;
        }

    }
}
