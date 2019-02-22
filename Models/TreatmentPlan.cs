﻿using System;
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
    }
}
