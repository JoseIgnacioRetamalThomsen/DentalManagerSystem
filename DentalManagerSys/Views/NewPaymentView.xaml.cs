using DataAccessLibrary;
using DentalManagerSys.ViewModel;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewPaymentView : Page
    {
        public NewPaymentViewModel ViewModel;
        public NewPaymentView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


            


            if (e.Parameter == null)
            {

            }
            else
            {
                ViewModel = new NewPaymentViewModel(e.Parameter.ToString());
            }

            TreatmentPlanDB.ItemsSource = ViewModel.TreatmentPlans;
            TreatmentPlanDB.SelectedItem = ViewModel.GetMostRecent();


        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("working");
            decimal amount = Convert.ToDecimal(Amount.Text);
            DAO.AddNewpayment(((TreatmentPlan)TreatmentPlanDB.SelectedItem).TreatmentPLanID,ViewModel.Customer.iD,amount,DateTime.Now.ToString());
            FireBaseDAO f = new FireBaseDAO();
            f.AddNewpayment(((TreatmentPlan)TreatmentPlanDB.SelectedItem).TreatmentPLanID, ViewModel.Customer.iD, amount, DateTime.Now.ToString());
            Frame.GoBack();
        }

        private void Amount_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = new String(sender.Text.Where(char.IsDigit).ToArray());
        }
    }
}
