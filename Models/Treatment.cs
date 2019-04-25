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
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Models
{
    public class Treatment //: INotifyPropertyChanged
    {
        public int ID { get; set; }


        public string Name { get; set; }
        private decimal _Price;

        
        public Treatment(int iD, string name, decimal price)
        {
            this.ID = iD;
            this.Name = name;
            this._Price = price;
        }

        public decimal Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
               // OnPropertyChanged();
            }
        }

        public String ShowPrice
        {
            set { }
           
            get
            {
                return _Price.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
                
    }
}
