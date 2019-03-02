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
using DentalManagerSys.ViewModel;
using Models;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;


namespace DentalManagerSys.Views
{

    /// <summary>
    /// Show details of customers, include treatments plans and paymenrs
    /// </summary>
    public sealed partial class ViewCustomerDetails : Page
    {
        /// <summary>
        /// Customer id
        /// </summary>
        private string iD;

        /// <summary>
        /// Model for data management
        /// </summary>
        public CustomerDetailsViewModel ViewModel { get; set; }

        /// <summary>
        /// Create page
        /// </summary>
        public ViewCustomerDetails()
        {
            this.InitializeComponent();

        }

        /// <summary>
        /// Call when naviagtion on page, Create ViewModel , Set sources for List
        /// View of plans and paymenrs
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            ViewModel = new CustomerDetailsViewModel();

            if (e.Parameter == null)
            {

            }
            else
            {
                Debug.WriteLine(e.Parameter);
                DisplayDetails(e.Parameter.ToString());
                iD = e.Parameter.ToString();
            }

            ViewModel.SetTreatmentsPlan();
            AcceptedTPListView.ItemsSource = ViewModel.AcceptedTreatmentPlans;
            CreatedTPListView.ItemsSource = ViewModel.CreatedTreatmentPlans;
            FinishTPListView.ItemsSource = ViewModel.FinishedTreatmentPlans;

            ViewModel.SetPayments();
            AllPaymentsLV.ItemsSource = ViewModel.PaymentsOC;

        }

        /// <summary>
        /// Display customer details in top
        /// </summary>
        /// <param name="iD"></param>
        private void DisplayDetails(string iD)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");
            string Home = resourceLoader.GetString("/Strings/Home");
            string Mobile = resourceLoader.GetString("/Strings/Mobile");

            Customer temp = DAO.GetCustomerByID(iD);
            //set customer on view model
            ViewModel.Customer = temp;

            PageTitle.Text = temp.name + " " + temp.surname;
            AdressDetails.Text = temp.street + ", " + temp.city + ", " + temp.province + ", " + temp.postcode + ", " + temp.country;

            IdTextBox.Text = temp.iD;
            DOBTextBox.Text = temp.dOB.ToString("dd/MM/yyyy");

            mobileTextBox.Text = Mobile + ": " + temp.mobileNum;
            fixTextBox.Text = Home + ": " + temp.homeNum;
            emailTextBox.Text = temp.email;
            commentsTextBox.Text = temp.comments;
        }


        /// <summary>
        /// Navigate to page where new treatment plan can be created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Send customer id as parameter</param>
        private void CreateTreatmentPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewTreatmentPlanView), iD,
                   new DrillInNavigationTransitionInfo());
        }



        /// <summary>
        /// Navigate to treatment on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptedTPListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (ListView)sender;
            int id = lv.SelectedIndex;
            string name = lv.Name;
                        
            TreatmentPlan tp = null;
            switch (name)
            {
                case "AcceptedTPListView":
                    tp = ViewModel.AcceptedTreatmentPlans[id];

                    break;
                case "CreatedTPListView":
                    tp = ViewModel.CreatedTreatmentPlans[id];
                    break;
                case "FinishTPListView":
                    tp = ViewModel.FinishedTreatmentPlans[id];
                    break;
            }

            Frame.Navigate(typeof(TreatmentPlanView), tp,
                  new DrillInNavigationTransitionInfo());

        }

        /// <summary>
        /// Navigate to view where new payment can be added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewPaymentView), ViewModel.Customer.iD,
                 new DrillInNavigationTransitionInfo());
        }
    }
}
