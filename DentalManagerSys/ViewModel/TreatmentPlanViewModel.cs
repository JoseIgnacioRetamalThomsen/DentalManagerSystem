using DataAccessLibrary;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagerSys.ViewModel
{
    public class TreatmentPlanViewModel : ViewModelBase
    {
        private static string created = "Created";
        private static string accepted = "Accepted";
        private static string done = "done";

        public decimal total;
        public decimal Total
        {
            get => total;
            set
            {
                total = value;
                OnPropertyChanged("Total");
                OnPropertyChanged("ShowTotal");
            }
        }
        public string ShowTotal
        {
            get
            {
                return total.ToString("C", CultureInfo.CurrentCulture);
            }
            set
            {

            }
        }
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
            //created,accepted,done
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

        public TreatmentPlanViewModel()
        {
            
        }
        public int PlanID { get; set; }

        public void LoadTreatments(int planID)
        {
            List<TreatmentOnPlan> top = DAO.GetTreatmentOnPlansByID(planID);
            TreatmentsOnPlan = new ObservableCollection<TreatmentOnPlan>( DAO.GetTreatmentOnPlansByID(planID));

            PlanID = planID;
            CalculateTotal();



        }

        public void CalculateTotal()
        {
            Total = 0;
            foreach(TreatmentOnPlan t in TreatmentsOnPlan)
            {
                Total += t.Price;
            }
        }

        public void ChangeState(TreatmentPlaneState state)
        {
            App.Data.UpdateTreatmentPlanState(state, PlanID);
           /* FireBaseDAO f = new FireBaseDAO();
            DAO.UpdateTreatmentPlanState(state,PlanID);
            f.UpdateTreatmentPlanState(state, PlanID);*/
        }
    }
}
