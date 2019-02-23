using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TreatmentOnPlan
    {
        public int iD { get; set; }


        public string name { get; set; }

        public decimal price;


        public TreatmentOnPlan(int iD, string name, decimal price)
        {
            this.iD = iD;
            this.name = name;
            this.price = price;
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
            }
        }


        public override string ToString()
        {
            return this.name;
        }
    }
}
