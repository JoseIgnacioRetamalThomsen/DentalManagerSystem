using DataAccessLibrary;
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
     
        public ViewCustomerDetails()
        {
            this.InitializeComponent();

            
        }

        private void DisplayDetails(string iD)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");
            string home = resourceLoader.GetString("/Strings/home");

            Customer temp = DAO.GetCustomerByID(iD);

            PageTitle.Text = temp.name + " " + temp.surname;
            AdressDetails.Text = temp.street + ", " + temp.city+", "+ temp.province + ", "+ temp.postcode + ", " + temp.country;

            IdTextBox.Text = temp.iD;
            DOBTextBox.Text = temp.dOB.ToString("dd/MM/yyyy");

           /* streetTextBox.Text = temp.street;
            cityTextBox.Text = temp.city;
            provinceTextBox.Text = temp.province;
            countryTextBox.Text = temp.country;
            postcodeTextBox.Text = temp.postcode;*/

            mobileTextBox.Text = temp.mobileNum;
            fixTextBox.Text = temp.homeNum;
            emailTextBox.Text = temp.email;
            commentsTextBox.Text = temp.comments;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
               
            }
            else
            {
               Debug.WriteLine(e.Parameter);
                DisplayDetails(e.Parameter.ToString());
                iD = e.Parameter.ToString();
            }

           
            
        }

        private void CreateTreatmentPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewTreatmentPlanView), iD,
                   new DrillInNavigationTransitionInfo());
        }
    }
}
