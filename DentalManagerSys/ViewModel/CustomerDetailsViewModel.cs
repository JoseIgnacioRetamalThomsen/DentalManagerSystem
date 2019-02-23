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
    public class CustomerDetailsViewModel : ViewModelBase
    {
      public   ObservableCollection<Payments> PaymentsOC { get; set; }

      public  Customer Customer { get; set; }

        public ObservableCollection<TreatmentPlan> treatmentPlans;

        public ObservableCollection<TreatmentPlan> TreatmentPlans
        {
            get
            {
                return treatmentPlans;
            }
            set
            {
                value = treatmentPlans;
            }
        }
        public ObservableCollection<TreatmentPlan> acceptedTreatmentPlans;

        public ObservableCollection<TreatmentPlan> AcceptedTreatmentPlans
        {
            get
            {
                return acceptedTreatmentPlans;
            }
            set
            {
                value = acceptedTreatmentPlans;
            }
        }
        public ObservableCollection<TreatmentPlan> finishedTreatmentPlans;
        public ObservableCollection<TreatmentPlan> FinishedTreatmentPlans
        {
            get
            {
                return finishedTreatmentPlans;
            }
            set
            {
                value = finishedTreatmentPlans;
            }
        }

        public ObservableCollection<TreatmentPlan> createdTreatmentPlans;
        public ObservableCollection<TreatmentPlan> CreatedTreatmentPlans
        {
            get
            {
                return createdTreatmentPlans;
            }
            set
            {
                value = createdTreatmentPlans;
            }
        }

        public CustomerDetailsViewModel()
        {
            
            //    new ObservableCollection<TreatmentPlan>()
            //{
            //    new TreatmentPlan("ID",TreatmentPlaneState.Created,DateTime.Now,DateTime.Now),
            //    new TreatmentPlan("ID2",TreatmentPlaneState.Created,DateTime.Now,DateTime.Now)
            //};
        }

        public void SetTreatmentsPlan()
        {
            treatmentPlans = new ObservableCollection<TreatmentPlan>(DAO.GetAllTreatmentPlansByID(Customer.iD));
            createdTreatmentPlans = new ObservableCollection<TreatmentPlan>();
            acceptedTreatmentPlans = new ObservableCollection<TreatmentPlan>();
            finishedTreatmentPlans = new ObservableCollection<TreatmentPlan>();

         

            foreach (TreatmentPlan tp in treatmentPlans)
            {
                switch (tp.State)
                {
                    case TreatmentPlaneState.Created:
                        createdTreatmentPlans.Add(tp);
                        break;
                    case TreatmentPlaneState.Accepted:
                        acceptedTreatmentPlans.Add(tp);
                        break;
                    case TreatmentPlaneState.Finish:
                        finishedTreatmentPlans.Add(tp);
                        break;

                }
            }
        }
        public void SetPayments()
        {
            PaymentsOC = new ObservableCollection<Payments>(DAO.GetAllPaymenyByCustomerID(Customer.iD));
            //    new ObservableCollection<Payments>()
            //{
            //    new Payments(1,1,"1",12321,DateTime.Now),
            //    new Payments(2,2,"2",2132131,DateTime.Now)
            //};
            //new ObservableCollection<Payments>(DAO.GetPaymenyByCustomerID(Customer.iD));
        }
    }
}
