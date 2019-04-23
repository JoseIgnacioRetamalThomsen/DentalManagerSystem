using DataAccessLibrary;
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
    public sealed partial class AdminAddress : Page
    {
        public AdminAddress()
        {
            this.InitializeComponent();
            string userName = DAO.GetUserID();
            PageTitle.Text = ""+ userName;
        }

        private void EditAddress_Click(object sender, RoutedEventArgs e)
        {
            SaveUserDetails.IsEnabled = true;
            EditBarButton.IsEnabled = false;
            EnableWrite();
        }

        private void SaveUserDetails_Click(object sender, RoutedEventArgs e)
        {
            EditBarButton.IsEnabled = true;
            SaveUserDetails.IsEnabled = false;
            SaveUserDetailsSQLite();
        }

        public void EnableWrite()
        {
            inputName.IsReadOnly = false;
            inputSurename.IsReadOnly = false;
            streetInput.IsReadOnly = false;
            cityInput.IsReadOnly = false;
            provinceInput.IsReadOnly = false;
            countryInput.IsReadOnly = false;
            postCodeInput.IsReadOnly = false;
            mobilNumInput.IsReadOnly = false;
            homeNumInput.IsReadOnly = false;
        }

        public void SaveUserDetailsSQLite()
        {
            /* DAO.AddNewCustomer(
                 idInput.Text, //id
                 inputName.Text, //name
                 inputSurename.Text,//surname
                 birthDatePicker.Date.ToString("yyyy-MM-dd"), //dob
                 streetInput.Text, //street address
                 cityInput.Text, //city
                 provinceInput.Text,//Province
                 countryInput.Text, //country
                 postCodeInput.Text,//postcode
                 mobilNumInput.Text,//mobil number
                 homeNumInput.Text,//home number
                 emaillInput.Text, //email
                 commentsInput.Text //email
                 ); */
        }
    }
}
