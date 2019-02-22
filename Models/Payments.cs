using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class Payments
    {
        public int paymentsID { get; set; }
        public int treatmentPlanID { get; set; }
        public string customerID { get; set; }
        public float amount { get; set; }
        public DateTime treatmentCompleteDate { get; set; }

        public Payments(int paymentsID, int treatmentPlanID,string customerID, float amount, DateTime treatmentCompleteDate)
        {
            this.paymentsID = paymentsID;
            this.treatmentPlanID = treatmentPlanID;
            this.customerID = customerID;
            this.amount = amount;
            this.treatmentCompleteDate = treatmentCompleteDate;

        }
    }
}
