///------------------------------------------
///
///  Dental Manager System
///  Profesional Practice in IT project
///  GMIT 2019
///  
///  Markm Ndpeanoch
///  Jose I. Retamal
///------------------------------------------
///
using DataAccessLibrary;
using DataAccessLibrary.REST;
using Models;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// Page for synchronize account.
    /// </summary>
    public sealed partial class SyncAccountView : Page
    {
        private string errorMessageSyncAccount;

        /// <summary>
        /// Constructor
        /// </summary>
        public SyncAccountView()
        {
            this.InitializeComponent();
            InitString();

        }

        /// <summary>
        /// Initialize string from resources.
        /// </summary>
        private void InitString()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");

            errorMessageSyncAccount = resourceLoader.GetString("/Strings/errorMessageSyncAccount/Text");
        }

        /// <summary>
        /// Syncronize the account, show error message or navigate to login page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SyncAccountButton_Click(object sender, RoutedEventArgs e)
        {
           
            var user = new User("no name", PasswordBox.Password, Email.Text);
            Res res = await Auth.SignIn(user);
            if(res.Success)
            {
                UserRes userData = await Auth.GetUser(user);
                
                //create user account
                User userForCreate = new User(userData.DisplayName, "none", userData.Email);
                DAO.AddNewUser(userForCreate);

                //navigate to login windows
                Frame.Navigate(typeof(LoginView));
            }
            else
            {
                ErrorMessage.Text = errorMessageSyncAccount;
            }
        }

        /// <summary>
        /// Back to account managemint page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
