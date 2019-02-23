using DataAccessLibrary;
using DentalManagerSys.ViewModel;
using Models;
using System;
using System.Collections.Generic;
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
    public sealed partial class PrinterView : Page
    {
        private string iD;
     

        public CustomerDetailsViewModel ViewModel { get; set; }

        public PrinterView()
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



            //PageTitle.Text = temp.name + " " + temp.surname;
            AdressDetails.Text = temp.street + ", " + temp.city + ", " + temp.province + ", " + temp.postcode + ", " + temp.country;

            //mobileTextBox.Text = Mobile + ": " + temp.mobileNum;
            fixTextBox.Text = Home + ": " + temp.homeNum;

            commentsTextBox.Text = temp.comments;
        }

       
    }




}
