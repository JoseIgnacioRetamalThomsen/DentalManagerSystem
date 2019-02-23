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

        public ObservableCollection<TreatmentOnPlan> TreatmentsOnPlan { get; set; }


        public DateTime completedTreatmentDate;
        public DateTime CompletedTreatmentDate
        {
            get => completedTreatmentDate;
            set
            {
                completedTreatmentDate = value;
                OnPropertyChanged("CompletedTreatmentDate");
            }
        }

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

        public void LoadTreatments(int planID)
        {
            TreatmentsOnPlan = new ObservableCollection<TreatmentOnPlan>()
            {
               new TreatmentOnPlan(1,2,200,Convert.ToDateTime("01/01/0001 00:00:00")),
               new TreatmentOnPlan(2,4,250,Convert.ToDateTime("01/01/0001 00:00:00")),
               new TreatmentOnPlan(3,6,200,Convert.ToDateTime("01/01/0001 00:00:00")),
            };
        }
    }
}
