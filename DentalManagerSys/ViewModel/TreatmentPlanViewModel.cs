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
        public decimal total;
        public decimal Total
        {
            get => total;
            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }

        public ObservableCollection<TreatmentOnPlanShow> TreatmentsOnPlan { get; set; }

        public TreatmentPlanShow TreatmentPlanForView { get; set; }

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
            List<TreatmentOnPlan> top = DAO.GetTreatmentOnPlansByID(planID);
            

            List<Treatment> treatments = DAO.GetAllTreatment();
            TreatmentPlanForView = new TreatmentPlanShow(ActualTreatmentPlan, Customer, top,treatments);
            TreatmentPlanForView.PrintConsole();
            TreatmentsOnPlan = new ObservableCollection<TreatmentOnPlanShow>(TreatmentPlanForView.Treatments);

            Total = TreatmentPlanForView.GetTotal();
           
        }
    }
}
