using DataAccessLibrary;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class AdminAddress : Page
    {
        string userName;
        DAO dao = new DAO();
        AdminDetails adminDetails = null;

        public AdminAddress()
        {
            this.InitializeComponent();
            userName = DAO.GetUserID();
            PageTitle.Text = ""+ userName;

            Boolean AdminExsitance = dao.CheckAdmidDetailsExist(userName);
            if (AdminExsitance == true)
            {
             adminDetails = dao.GetAdminDetails(userName);

                inputName.Text = adminDetails.firstName;
                inputSurename.Text = adminDetails.surname;
                streetInput.Text = adminDetails.street;
                cityInput.Text = adminDetails.city;
                provinceInput.Text = adminDetails.province;
                countryInput.Text = adminDetails.country;
                postCodeInput.Text = adminDetails.postcode;
                mobilNumInput.Text = adminDetails.mobileNum;
                homeNumInput.Text = adminDetails.homeNum;
            }
           
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
            DisEnableWrite();
            SuccesfullEditAlert();
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

        public void DisEnableWrite()
        {
            inputName.IsReadOnly = true;
            inputSurename.IsReadOnly = true;
            streetInput.IsReadOnly = true;
            cityInput.IsReadOnly = true;
            provinceInput.IsReadOnly = true;
            countryInput.IsReadOnly = true;
            postCodeInput.IsReadOnly = true;
            mobilNumInput.IsReadOnly = true;
            homeNumInput.IsReadOnly = true;
        }

        private async void SuccesfullEditAlert()
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = "SUCCESSFUL",
                Content = "Your Details Was Successfully Updated!",
                CloseButtonText = "Ok"
            };

            await noWifiDialog.ShowAsync();
        }

        public void SaveUserDetailsSQLite()
        {
            Boolean AdminExsitance = dao.CheckAdmidDetailsExist(userName);
            if (AdminExsitance == false)
            {
                dao.NewAdminDetails(
             inputName.Text, //name
             inputSurename.Text,//surname
             streetInput.Text, //street address
             cityInput.Text, //city
             provinceInput.Text,//Province
             countryInput.Text, //country
             postCodeInput.Text,//postcode
             mobilNumInput.Text,//mobil number
             homeNumInput.Text,//home number
             userName //email
             );
            }
            else
            {
            dao.UpdateAdminDetails(
            inputName.Text, //name
            inputSurename.Text,//surname
            streetInput.Text, //street address
            cityInput.Text, //city
            provinceInput.Text,//Province
            countryInput.Text, //country
            postCodeInput.Text,//postcode
            mobilNumInput.Text,//mobil number
            homeNumInput.Text,//home number
            userName //email
            );
            }
        
        }
    }
}
