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
    public sealed partial class EditCustomerDetails : Page
    {
        private string iD;
        public EditCustomerDetails()
        {
            this.InitializeComponent();
            DisplayDetails("G0035");
        }

        private void DisplayDetails(string iD)
        {
            
            Customer temp = DAO.GetCustomerByID(iD);
            
            PageTitle.Text = temp.name + " " + temp.surname;
            DOBTextBox.Text = temp.dOB.ToString();
            streetTextBox.Text = temp.street;
            cityTextBox.Text = temp.city;
            provinceTextBox.Text = temp.province;
            countryTextBox.Text = temp.country;
            postcodeTextBox.Text = temp.postcode;
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
              
            }

        }


        private void CreateTreatmentPlan_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(NewTreatmentPlanView), iD,
            new DrillInNavigationTransitionInfo());
        }

    }
}
