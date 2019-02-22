using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagerSys.ViewModel
{
    public class TreammentPlanViewModel : ViewModelBase
    {

        public TreatmentPlaneState treatmentPlanState;

        public TreatmentPlaneState TreatmentPLanState
        {
            get => treatmentPlanState;
            set
            {
                treatmentPlanState = value;
                OnPropertyChanged("TreatmentPLanState");
            }
        }



        public TreatmentPlan actualTreatmentPlan;

        public TreatmentPlan ActualTreatmentPlan
        {
            get => actualTreatmentPlan;
            set
            {
                actualTreatmentPlan = value;
                OnPropertyChanged("ActualTreatmentPlan");
            }
        }
    }
}
