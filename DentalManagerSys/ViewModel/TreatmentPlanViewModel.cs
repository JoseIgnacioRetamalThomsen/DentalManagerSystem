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
    public class TreatmentPlanViewModel : ViewModelBase
    {

        public ObservableCollection<Treatment> treatmentsOnPlan;


        public string CustomerName { get; set; }

        public Customer Customer { get; set; }
        public TreatmentPlaneState actualTreatmentPlanState;

        public ObservableCollection<TreatmentPlaneState> treatmentsPlans = new ObservableCollection<TreatmentPlaneState>()
        {
            TreatmentPlaneState.Accepted,TreatmentPlaneState.Created,TreatmentPlaneState.Finish
        };

        public ObservableCollection<TreatmentPlaneState> TreatmentsPlans
        {
            get => treatmentsPlans;
            set
            {
                treatmentsPlans = value;
                OnPropertyChanged("TreatmentsPlans");
            }
        }
        

        public TreatmentPlaneState ActualTreatmentPlanState
        {
            get => actualTreatmentPlanState;
            set
            {
                actualTreatmentPlanState = value;
                OnPropertyChanged("TreatmentPLanState");
                Debug.WriteLine("sdsfasdafasdfdsa6666666666666666"+actualTreatmentPlanState);
            }
        }



        public TreatmentPlan actualTreatmentPlan;

        public TreatmentPlan ActualTreatmentPlan
        {
            get => actualTreatmentPlan;
            set
            {
                actualTreatmentPlan = value;
                OnPropertyChanged("ActualTreatmentPlan");
                Customer = DAO.GetCustomerByID(actualTreatmentPlan.CustomerID);
                CustomerName = Customer.name + " " + Customer.surname;
                actualTreatmentPlanState = actualTreatmentPlan.State;
            }
        }
    }
}
