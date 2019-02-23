using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public  class TreatmentOnPlan
    {
        public int TreatmentPlanId { get; set; }
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

        public TreatmentOnPlan(int treatmentPlanId, int treatnentID, decimal price, DateTime completedDate)
        {
            TreatmentPlanId = treatmentPlanId;
            TreatmentID = treatnentID;
            Price = price;
            CompletedDate = completedDate;
        }


    }
}
