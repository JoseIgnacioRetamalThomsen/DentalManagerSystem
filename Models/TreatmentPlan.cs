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
        public string CustomerID { get; set; }
        public TreatmentPlaneState State { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime treatmentPlanCompleteDate { get; set; }

        public TreatmentPlan(string customerID, TreatmentPlaneState state, DateTime creationDate, DateTime treatmentPlanCompleteDate)
        {
            CustomerID = customerID;
            State = state;
            this.creationDate = creationDate;
            this.treatmentPlanCompleteDate = treatmentPlanCompleteDate;
        }
    }
}
