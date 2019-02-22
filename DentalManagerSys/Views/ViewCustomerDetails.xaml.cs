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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
   
    public sealed partial class ViewCustomerDetails : Page
    {
        private string iD;

        public CustomerDetailsViewModel ViewModel { get; set; }

        public ViewCustomerDetails()
        {
            this.InitializeComponent();

            

        }

        private void DisplayDetails(string iD)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");
            string Home = resourceLoader.GetString("/Strings/Home");
            string Mobile = resourceLoader.GetString("/Strings/Mobile");

   

            Customer temp = DAO.GetCustomerByID(iD);

            //set customer on view model
            ViewModel.Customer = temp;

            PageTitle.Text = temp.name + " " + temp.surname;
            AdressDetails.Text = temp.street + ", " + temp.city+", "+ temp.province + ", "+ temp.postcode + ", " + temp.country;

            IdTextBox.Text = temp.iD;
            DOBTextBox.Text = temp.dOB.ToString("dd/MM/yyyy");

            mobileTextBox.Text = Mobile +": "+ temp.mobileNum;
            fixTextBox.Text = Home +": " +temp.homeNum;
            emailTextBox.Text = temp.email;
            commentsTextBox.Text = temp.comments;
        }

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

        }

        private void CreateTreatmentPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewTreatmentPlanView), iD,
                   new DrillInNavigationTransitionInfo());
        }

       
        

        private void AcceptedTPListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                ListView lv = (ListView)sender;
            int id = lv.SelectedIndex;
            string name = lv.Name;

           Debug.WriteLine("working777777777777777777777777777777777777777777777777777777777777777777777777777777" + id);

            TreatmentPlan tp = null;
            switch (name)
            {
                case "AcceptedTPListView":
                    tp = ViewModel.AcceptedTreatmentPlans[id];
                    Debug.WriteLine("working777777777777777777777777777777777777777777777777777777777777777777777777777777" + name);
                    break;

            }

            Frame.Navigate(typeof(TreatmentPlanView), tp,
                  new DrillInNavigationTransitionInfo());
            Debug.WriteLine("working777777777777777777777777777777777777777777777777777777777777777777777777777777" + name); 
        }
    }
}
