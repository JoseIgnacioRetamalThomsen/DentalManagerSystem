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

using DataAccessLibrary;
using Models;
using System.Collections.ObjectModel;

namespace DentalManagerSys.ViewModel
{
    public class TreatmentViewModel : ViewModelBase
    {
        /// <summary>
        /// Treatmets list on DB
        /// </summary>
        public ObservableCollection<Treatment> TreatmentsList { get; set; } = null;

        /// <summary>
        /// Create instance, populate TreatmentList from database
        /// </summary>
        public TreatmentViewModel()
        {
            TreatmentsList = new ObservableCollection<Treatment>(DAO.GetAllTreatment());
        }
    }
}
