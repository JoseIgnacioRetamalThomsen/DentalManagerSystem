using DataAccessLibrary;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagerSys.ViewModel
{
    /// <summary>
    /// View model for edit treatments
    /// </summary>
    public class EditTreatmentViewModel: ViewModelBase
    {
        /// <summary>
        /// ID of the edited treament, is set when created
        /// </summary>
        public Treatment TreamentOnModel{ get; set; }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private decimal _Price;
        public decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public EditTreatmentViewModel(Treatment treatment)
        {
            TreamentOnModel = treatment;
            
            Name = TreamentOnModel.Name;
            Price = TreamentOnModel.Price;
        }

        internal void SaveTreatment(string text, decimal v)
        {
            App.Data.UpdateTreatment(TreamentOnModel.ID, text, v);

            //FireBaseDAO f = new FireBaseDAO();
            //FireBaseDAO f = new FireBaseDAO();
            //f.UpdateTreatment(new Treatment(TreamentOnModel.ID, text, v));
           // DAO.UpdateTreatment(new Treatment(TreamentOnModel.ID, text, v));
        }
    }
}
