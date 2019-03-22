///------------------------------------------
///
///  Dental Manager System
///  Profesional Practice in IT project
///  GMIT 2019
///  
///  Markm Ndpeanoch
///  Jose I. Retamal
///------------------------------------------
///


using DentalManagerSys.ViewModel;
using Models;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TreatmentPlanView : Page
    {
        /// <summary>
        /// View model for this pages
        /// </summary>
        TreatmentPlanViewModel ViewModel { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TreatmentPlanView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// When navigate to this page, treament plan data is receive
        /// from navigation parameters
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = new TreatmentPlanViewModel();


            if (e.Parameter == null)
            {

            }
            else
            {
                ViewModel.ActualTreatmentPlan = (TreatmentPlan)e.Parameter;
            }

            Debug.WriteLine(ViewModel.ActualTreatmentPlan.CustomerID);

            //load treatments on plan
            ViewModel.LoadTreatments(ViewModel.ActualTreatmentPlan.TreatmentPLanID);
            //treatment state combo box
            TreatmentStateCB.ItemsSource = ViewModel.TreatmentsPlans;
            TreatmentStateCB.SelectedItem = ViewModel.ActualTreatmentPlanState;

            //treatments lv
            TreatmentsOnPlanLV.ItemsSource = ViewModel.TreatmentsOnPlan;
        }

        /// <summary>
        /// Change state of treatment plan, 
        /// the new state is save to database each time the selection changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreatmentStateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ActualTreatmentPlanState = (TreatmentPlaneState)((ComboBox)sender).SelectedItem;
            ViewModel.ChangeState((TreatmentPlaneState)((ComboBox)sender).SelectedItem);
        }

        /// <summary>
        /// When user select a treatment plan, it can change the state to done
        /// this will set done date to acutal date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreatmentsOnPlanLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected item index
            int index = TreatmentsOnPlanLV.SelectedIndex;

            //get treatement on plan
            TreatmentOnPlan top = ViewModel.TreatmentsOnPlan[index];

            //activate button for mark as done if is not done
            if (!top.IsDone)
            {
                CreateTreatmentPlanCompletedB.IsEnabled = true;
                CreateTreatmentPlanNotCopletedB.IsEnabled = false;
            }
            else
            {
                CreateTreatmentPlanCompletedB.IsEnabled = false;
                CreateTreatmentPlanNotCopletedB.IsEnabled = true;
            }
        }

        /// <summary>
        /// Mark a treatment as not completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateTreatmentPlanNotCopletedB_Click(object sender, RoutedEventArgs e)
        {
            int index = TreatmentsOnPlanLV.SelectedIndex;
            TreatmentOnPlan top = ViewModel.TreatmentsOnPlan[index];
            top.IsDone = false;

            App.Data.UpdateTreatmentOnPlan(top);

            ReloadListView();
        }

        /// <summary>
        /// Reload list of treaments
        /// </summary>
        private void ReloadListView()
        {
            TreatmentsOnPlanLV.SelectionChanged -= TreatmentsOnPlanLV_SelectionChanged;
            TreatmentsOnPlanLV.ItemsSource = null;
            TreatmentsOnPlanLV.ItemsSource = ViewModel.TreatmentsOnPlan;
            TreatmentsOnPlanLV.SelectionChanged += TreatmentsOnPlanLV_SelectionChanged;
        }

        private void CreateTreatmentPlanCompletedB_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("click");
            //get selected item index
            int index = TreatmentsOnPlanLV.SelectedIndex;
            TreatmentOnPlan top = ViewModel.TreatmentsOnPlan[index];
            top.CompletedDate = DateTime.Now;
            top.IsDone = true;
            App.Data.UpdateTreatmentOnPlan(top);
            /* DAO.UpdateTreatmentOnPlan(top);
             FireBaseDAO f = new FireBaseDAO();
             f.UpdateTreatmentOnPlan(top);*/
            ReloadListView();


        }

        private void EditPlanDone_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();

        }

        private void NewPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewPaymentView), new NewPaymentData(ViewModel.Customer.iD, ViewModel.ActualTreatmentPlan.TreatmentPLanID, true),
                new DrillInNavigationTransitionInfo());
        }
    }
}
