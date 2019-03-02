///------------------------------------------
///
///  Dental Manager System
///  Profesional Practice in IT project
///  GMIT 2019
///  
///  Markm Ndipenoch
///  Jose I. Retamal
///------------------------------------------
///

using DataAccessLibrary;
using DentalManagerSys.ViewModel;
using Models;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace DentalManagerSys.Views
{
    /// <summary>
    /// View for create new payments
    /// </summary>
    public sealed partial class NewPaymentView : Page
    {
        /// <summary>
        /// The View model
        /// </summary>
        public NewPaymentViewModel ViewModel;

        /// <summary>
        /// Create pages
        /// </summary>
        public NewPaymentView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Call on navigated , initializate the view and set sources for list views
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {

            }
            else
            {
                ViewModel = new NewPaymentViewModel((NewPaymentData)e.Parameter);
            }

            TreatmentPlanDB.ItemsSource = ViewModel.TreatmentPlans;
            if (ViewModel.TreatmentPlans.Count > 0)
                TreatmentPlanDB.SelectedItem = ViewModel.GetMostRecent();
        }

        /// <summary>
        /// Create new payment on DB with data from view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            decimal amount = Convert.ToDecimal(Amount.Text);
            DAO.AddNewpayment(((TreatmentPlan)TreatmentPlanDB.SelectedItem).TreatmentPLanID, ViewModel.Customer.iD, amount, DateTime.Now.ToString());
            Frame.GoBack();
        }

        /// <summary>
        /// Allow only number in amount input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Amount_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = new String(sender.Text.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
