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

using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public enum TreatmentPlaneState
    {
        Created, Accepted, Finish
    }
    public class TreatmentPlan
    {
        public int TreatmentPLanID { get; set; }
        public string CustomerID { get; set; }
        public TreatmentPlaneState State { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime TreatmentPlanCompleteDate { get; set; }
        public int IDForCustomer { get; set; }

        public TreatmentPlan(string customerID, TreatmentPlaneState state, DateTime creationDate, DateTime treatmentPlanCompleteDate)
        {
            CustomerID = customerID;
            State = state;
            this.CreationDate = creationDate;
            this.TreatmentPlanCompleteDate = treatmentPlanCompleteDate;
        }

        public TreatmentPlan(int treatementPLanID,string customerID, TreatmentPlaneState state, DateTime creationDate, DateTime treatmentPlanCompleteDate)
        {
            this.TreatmentPLanID =treatementPLanID;
            CustomerID = customerID;
            State = state;
            this.CreationDate = creationDate;
            this.TreatmentPlanCompleteDate = treatmentPlanCompleteDate;
        }

        public TreatmentPlan(int treatmentPLanID, string customerID, TreatmentPlaneState state, DateTime creationDate, DateTime treatmentPlanCompleteDate, int iDForCustomer) : this(treatmentPLanID, customerID, state, creationDate, treatmentPlanCompleteDate)
        {
            IDForCustomer = iDForCustomer;
        }

        public override string ToString()
        {
            return "ID= "+ IDForCustomer + " " + CreationDate.ToString("dd/MM/yyyy") ;
        }
    }
}
