using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Models
{
    public class TreatmentPlanShow
    {
        public TreatmentPlan TreatmentPlan { get; set; }
        public Customer Customer { get; set; }
        public List<TreatmentOnPlanShow> Treatments { get; set; }

        public TreatmentPlanShow(TreatmentPlan treatmentPlan, Customer customer, List<TreatmentOnPlan> treatmentsOnPlan,List<Treatment> treatmentList )
        {
            TreatmentPlan = treatmentPlan;
            Customer = customer;
            Treatments = new List<TreatmentOnPlanShow>();

            foreach (TreatmentOnPlan top in treatmentsOnPlan)
            {
               
                foreach(Treatment treatment in treatmentList)
                {
                    if(treatment.ID == top.TreatmentID)
                    {
                        Treatments.Add(new TreatmentOnPlanShow(top, treatment.Name));
                    }
                }
                
            }
        }

        public void PrintConsole()
        {
           Debug.WriteLine( TreatmentPlan.ToString());
           Debug.WriteLine( Customer.ToString());
            foreach (var t in Treatments)
            {
                Debug.WriteLine(t.treatmentName+" " +t.Treatment);
            }
          
        }

        public decimal GetTotal()
        {
            decimal total = 0;
            foreach(var tos in Treatments)
            {
                total += tos.Treatment.Price;
            }
            return total;
        }
    }
}
