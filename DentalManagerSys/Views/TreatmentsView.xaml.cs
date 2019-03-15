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
using DentalManagerSys.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Models;
using Windows.UI.Xaml.Media.Animation;

namespace DentalManagerSys.Views
{
    /// <summary>
    /// View for all treatmets
    /// </summary>
    public sealed partial class ManageTreatmentsView : Page
    {
        //Magane data for this view        
        public TreatmentViewModel ViewModel { get; set; }

        /// <summary>
        /// Initializes the page.
        /// </summary>
        public ManageTreatmentsView()
        {
            this.InitializeComponent();
       
            ViewModel = new TreatmentViewModel();

        }

        /// <summary>
        /// Navigate to Create New treatment view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewTreatment));
        }

        /// <summary>
        /// Navigate to Edit treatment view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTreatmentButton_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(EditTreatmentView), ((Treatment)DataGrid.SelectedItem),
                    new DrillInNavigationTransitionInfo());
        }

        /// <summary>
        /// Activate EditTreatmentButton button when there is a item selected in view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditTreatmentButton.IsEnabled = true;
        }
    }
}
