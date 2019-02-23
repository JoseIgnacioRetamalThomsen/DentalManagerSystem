using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public  class TreatmentOnPlan
    {
        public int TreatmentPlanTreatmentsID { get; set; }
        public int TreatmentPlanID { get; set; }
        public int TreatmentID { get; set; }
        public decimal Price { get; set; }
        public DateTime CompletedDate { get; set; }


        public bool IsDone
        {
            get
            {
                if (CompletedDate.ToString().Equals("01/01/0001 00:00:00"))
                    return false;
                return true;
            }
            set
            {
                ;
            }
        }

        public TreatmentOnPlan(int treatmentPlanTreatmentsID, int treatmentPlanID, int treatmentID, decimal price, DateTime completedDate)
        {
            TreatmentPlanTreatmentsID = treatmentPlanTreatmentsID;
            TreatmentPlanID = treatmentPlanID;
            TreatmentID = treatmentID;
            Price = price;
            CompletedDate = completedDate;
        }


    }
}
