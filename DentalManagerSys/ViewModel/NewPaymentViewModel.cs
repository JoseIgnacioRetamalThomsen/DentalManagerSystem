///------------------------------------------
///
///  Dental Manager System
///  Profesional Practice in IT project
///  GMIT 2019
///  
///  Markm Ndipenoch
///  Jose I. Retamal
///------------------------------------------
///

using DataAccessLibrary;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace DentalManagerSys.ViewModel
{
    public class NewPaymentViewModel : ViewModelBase
    {
        /// <summary>
        /// Amount of payment
        /// </summary>
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

        /// <summary>
        /// Customer who made payment
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Treantmets plan of the customer
        /// </summary>
        public List<TreatmentPlan> TreatmentPlans { get; set; }

        public string CustomerName
        {
            get
            {
                return Customer.name + " " + Customer.surname;
            }
        }
        NewPaymentData Data { get; set; }
        /// <summary>
        /// Create View model
        /// </summary>
        /// <param name="customerID">Id of customer of new payment</param>
        public NewPaymentViewModel(NewPaymentData data)
        {
            Data = data;
            Customer = DAO.GetCustomerByID(data.CustomerId);
            TreatmentPlans = DAO.GetAllTreatmentPlansByID(data.CustomerId);

        }

        /// <summary>
        /// Get most recent tratment plan
        /// </summary>
        /// <returns>Most recent Accpeted treatment plan</returns>
        public TreatmentPlan GetMostRecent()
        {
            TreatmentPlan mostResent = TreatmentPlans[0];
            if (Data.IsTreatmentID)
            {
                foreach (var tp in TreatmentPlans)
                {
                    if (tp.TreatmentPLanID == Data.TreatmentId)
                    {
                        mostResent = tp;
                    }
                }
            }
            else
            {

                foreach (var tp in TreatmentPlans)
                {
                    if (TreatmentPlans.IndexOf(tp) == 0) continue;

                    if (tp.State == TreatmentPlaneState.Accepted && tp.CreationDate < mostResent.CreationDate)
                    {
                        mostResent = tp;
                    }
                }
            }
            return mostResent;
        }
    }
}
