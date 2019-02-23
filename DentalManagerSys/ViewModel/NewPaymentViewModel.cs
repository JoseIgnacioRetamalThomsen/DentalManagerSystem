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
    public class NewPaymentViewModel:ViewModelBase
    {
        public decimal amount;
        public Decimal Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
                Debug.WriteLine("Amount changed=" + amount);
            }
        }

        public Customer Customer { get; set; }

        public List<TreatmentPlan> TreatmentPlans { get; set; }

        public string CustomerName
        {
            get
            {
                return Customer.name + " "+Customer.surname;
            }
        }

        public NewPaymentViewModel(string customerID)
        {
            Customer = DAO.GetCustomerByID(customerID);
            TreatmentPlans = DAO.GetAllTreatmentPlansByID(customerID);
 
        }
        public TreatmentPlan GetMostRecent()
        {

            TreatmentPlan mostResent = TreatmentPlans[0];

            foreach (var tp in TreatmentPlans)
            {
                if (TreatmentPlans.IndexOf(tp) == 0) continue;

                if (tp.State == TreatmentPlaneState.Accepted && tp.CreationDate > mostResent.CreationDate)
                {
                    mostResent = tp;
                }
            }
            return mostResent;
        }
    }
}
