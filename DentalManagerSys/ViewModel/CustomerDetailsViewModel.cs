using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagerSys.ViewModel
{
    public  class CustomerDetailsViewModel : ViewModelBase
    {
        public ObservableCollection<TreatmentPlan> treatmentPlans;

        public ObservableCollection<TreatmentPlan> TreatmentPlans
        {
            get
            {
                return treatmentPlans;
            }
            set
            {
                value = treatmentPlans;
            }
        }

        public CustomerDetailsViewModel()
        {
            treatmentPlans = new ObservableCollection<TreatmentPlan>()
            {
                new TreatmentPlan("ID",TreatmentPlaneState.Created,DateTime.Now,DateTime.Now),
                new TreatmentPlan("ID2",TreatmentPlaneState.Created,DateTime.Now,DateTime.Now)
            };
        }
    }
}
