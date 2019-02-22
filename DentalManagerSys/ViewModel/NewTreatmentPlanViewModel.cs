using DataAccessLibrary;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            DAO.AddNewTreatmentPlan(ActualCustomer.iD,"Created",DateTime.Now.ToString(), DateTime.Now.ToString());
            foreach(Treatment t in treatmentsOnPlan)
            {

                DAO.AddNewTreatmentPlanTreatments(2,Convert.ToInt32(t.iD),t.price, DateTime.Now.ToString());
            }
        }
    }
}
