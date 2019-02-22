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
        public TreatmentPlaneState state { get; set; }
        public string creationDate { get; set; }
        public string treatmentPlanCompleteDate { get; set; }


    }
}
