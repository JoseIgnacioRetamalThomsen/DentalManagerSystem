using DataAccessLibrary;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagerSys.ViewModel
{
    public class NewTreatmentPlanViewModel : ViewModelBase
    {
        public Customer actualCustomer;
        public Customer ActualCustomer
        {
            get
            {
                return actualCustomer;
            }
            set
            {
                actualCustomer = value;
                OnPropertyChanged("ActualCustomer");
            }
        }

        /// <summary>
        /// Total price for plan
        /// </summary>
        public decimal total = 0;

        public Decimal Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
                OnPropertyChanged("Total");
                OnPropertyChanged("ShowTotal");
            }
        }
        public string ShowTotal
        {
            get
            {
                return total.ToString("C", CultureInfo.CurrentCulture);
            }
            set
            {
                ;
            }
        }

        public ObservableCollection<TreatmentOnPlan> treatmentsOnPlan;
        public ObservableCollection<TreatmentOnPlan> TreatmentsOnPlan
        {
            get => treatmentsOnPlan;
            set
            {
                treatmentsOnPlan = value;
                OnPropertyChanged("TreatmentsOnPLan");
            }
        }

        private decimal _PriceBefore;
        public decimal PriceBefore
        {
            get => _PriceBefore;
            set
            {
                _PriceBefore = value;
                OnPropertyChanged(nameof(PriceBefore));

            }
        }

        private Treatment _BeforeTreatment;
        public Treatment BeforeTreatment
        {
            get => _BeforeTreatment;
            set
            {
                _BeforeTreatment = value;
                OnPropertyChanged(nameof(BeforeTreatment));

            }
        }
        public int Tooth { get; set; }
        public String Comments { get; set; }
        public ObservableCollection<String> Tooths { get; set; }
        public NewTreatmentPlanViewModel()
        {
            treatmentsOnPlan = new ObservableCollection<TreatmentOnPlan>();

            //Add tooths number to collection
            Tooths = new ObservableCollection<string>();
            for (int i = 1; i <= 32; i++)
            {
                Tooths.Add(i.ToString());

            }
        }


        public void CreateNewTreatmentPlan()
        {

            FireBaseDAO f = new FireBaseDAO();
            //int id = (int)DAO.AddNewTreatmentPlan(ActualCustomer.iD, (int)TreatmentPlaneState.Created, DateTime.Now.ToString(), "0");

            ////Add new Treatmentplan to firebase.
            //f.AddNewTreatmentPlan(id, ActualCustomer.iD, (int)TreatmentPlaneState.Created, DateTime.Now.ToString(), "0");

            int id = (int)App.Data.AddNewTreatmentPlan(ActualCustomer.iD, (int)TreatmentPlaneState.Created, DateTime.Now.ToString(), "0");

            foreach (TreatmentOnPlan top in treatmentsOnPlan)
            {

                top.TreatmentPlanID = id;
                App.Data.AddNewTreatmentPlanTreatments(top);
                //DAO.AddNewTreatmentPlanTreatments(top);
                //f.AddNewTreatmentPlanTreatments(top);

            }


        }

        public void RecalculateTotal()
        {
          Total = 0;
           foreach (TreatmentOnPlan t in TreatmentsOnPlan)
            {
               Total += t.Price;
            }
        }

        internal void AddTreatment()
        {
            TreatmentsOnPlan.Add(new TreatmentOnPlan(0,0,BeforeTreatment.ID,
                PriceBefore,DateTime.Now,Tooth, Comments, false,BeforeTreatment.Name));
            RecalculateTotal();
           
        }
    }
}
