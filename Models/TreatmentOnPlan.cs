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

        public int Tooth { get; set; }
        public string Comment { get; set; }
        public bool isDone { get; set; }
        public string Name { get; set; }

        public String IsDoneString
        {
            get
            {
                if (CompletedDate.ToString().Equals("01/01/0001 00:00:00"))
                    return "Not Completed";
                return CompletedDate.ToString("dd/MM/yyyy");
            }
            set
            {
                ;
            }
        }

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

        public TreatmentOnPlan(int treatmentPlanTreatmentsID, int treatmentPlanID, int treatmentID, decimal price, DateTime completedDate, int tooth, string comment, bool isDone,string name) : this(treatmentPlanTreatmentsID, treatmentPlanID, treatmentID, price, completedDate)
        {
            Tooth = tooth;
            Comment = comment;
            IsDone = isDone;
            Name = name;
        }
    }
}
