using DataAccessLibrary;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagerSys.ViewModel
{
    public class NewTreatmentPlanViewModel : ViewModelBase
    {
        public Customer actualCustomer;
        public Customer ActualCustomer
        {
            get
            {
                return actualCustomer;
            }
            set
            {
                actualCustomer = value;
                OnPropertyChanged("ActualCustomer");
            }
        }

        /// <summary>
        /// Total price for plan
        /// </summary>
        public decimal total=0;

        public Decimal Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }

        public ObservableCollection<Treatment> treatmentsOnPlan;
        public ObservableCollection<Treatment> TreatmentsOnPlan
        {
            get => treatmentsOnPlan;
            set {
                 treatmentsOnPlan= value;
                OnPropertyChanged("TreatmentsOnPLan");
            }
        }


        public NewTreatmentPlanViewModel()
        {
            treatmentsOnPlan = new ObservableCollection<Treatment>();
        }

            
        public void CreateNewTreatmentPlan()
        {
            int id = (int)DAO.AddNewTreatmentPlan(ActualCustomer.iD,"Created",DateTime.Now.ToString(), "0");
          
            foreach(Treatment t in treatmentsOnPlan)
            {

               DAO.AddNewTreatmentPlanTreatments(id,Convert.ToInt32(t.iD),t.price, "0");
            }
        }

        public void RecalculateTotal()
        {
            Total = 0;
            foreach(Treatment t in TreatmentsOnPlan)
            {
                Total += t.Price;
            }
        }
    }
}
