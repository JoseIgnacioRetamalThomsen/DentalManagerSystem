///------------------------------------------
///
///  Dental Manager System
///  Profesional Practice in IT project
///  GMIT 2019
///  
///  Markm Ndpenoch
///  Jose I. Retamal
///------------------------------------------
///

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Models
{
   public  class TreatmentOnPlan
    {
        public int TreatmentPlanTreatmentsID { get; set; }
        public int TreatmentPlanID { get; set; }
        public int TreatmentID { get; set; }
       
        public decimal Price { get; set; }

        public string ShowPrice
        {
            get
            {
                return Price.ToString("C", CultureInfo.CurrentCulture);
            }
            set
            {
                ;
            }
        }
        public DateTime CompletedDate { get; set; }

        public int Tooth { get; set; }
        public string Comment { get; set; }
       // public bool _IsDone { get; set; }
        public string Name { get; set; }

        public String IsDoneString
        {
            get
            {
                if (!IsDone)
                    return "Not Completed";
                return CompletedDate.ToString("dd/MM/yyyy");
            }
            set
            {
                ;
            }
        }

        public bool IsDone { get; set; }
     
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
