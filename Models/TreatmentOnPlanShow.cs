

using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TreatmentOnPlanShow
    {
        public TreatmentOnPlan Treatment { get; set; }
        public string treatmentName { get; set; }

        public TreatmentOnPlanShow(TreatmentOnPlan treatment, string treatmentName)
        {
            Treatment = treatment;
            this.treatmentName = treatmentName;
        }
    }
}
