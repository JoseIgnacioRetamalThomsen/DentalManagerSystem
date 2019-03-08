using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagerSys.ViewModel
{
    public class CreateUserViewModel: ViewModelBase
    {
        private String _Name;
        public String Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
                
        }


        private String _Password;
        public String Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged(nameof(Password));
            }

        }

        public void test()
        {
            Debug.WriteLine(_Name + " " + Password);
        }
    }
}
