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
            DisplayDetails();
        }

        private void DisplayDetails()
        {
            Customer temp = DAO.GetCustomerByID(iD);

            IdTextBox.Text = temp.iD;
            DOBTextBox.Text = temp.dOB.ToString();
            emailTextBox.Text = temp.email;
            mobileTextBox.Text = temp.mobileNum;
            fixTextBox.Text = temp.homeNum;
            streetTextBox.Text = temp.street;
            cityTextBox.Text = temp.city;
            provinceTextBox.Text = temp.province;
            postcodeTextBox.Text = temp.postcode;
            countryTextBox.Text = temp.country;
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
                iD = e.Parameter.ToString();
            }

           
            
        }
    }
}
