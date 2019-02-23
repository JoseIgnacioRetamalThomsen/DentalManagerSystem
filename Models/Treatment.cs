using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Models
{
    public class Treatment //: INotifyPropertyChanged
    {
        public int iD { get; set; }


        public string name { get; set; }

        public decimal price;

        //public event PropertyChangedEventHandler PropertyChanged = delegate { };


        public Treatment(int iD, string name, decimal price)
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
               // OnPropertyChanged();
            }
        }


        public override string ToString()
        {
            return this.name;
        }


        //public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    // Raise the PropertyChanged event, passing the name of the property whose value has changed.
        //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}


    }
}
