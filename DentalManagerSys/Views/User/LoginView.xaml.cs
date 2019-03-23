using DataAccessLibrary;
using DataAccessLibrary.REST;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
    public sealed partial class LoginView : Page
    {
        string createAccount;
        string wrongPassword;
        public LoginView()
        {
            this.InitializeComponent();
            DAO.InitializeDatabase();
            InitString();


        }

        private void InitString()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");

            wrongPassword = resourceLoader.GetString("/Strings/wrongPassword/Text");
            createAccount = resourceLoader.GetString("/Strings/createAccount/Text");
           // wrongPassword = "Wrong password";
            //createAccount = "Please click on Account Management for create your account";
        }

        User user;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // get user data from local database
            user = DAO.GetUser();
            if (user != null)
            {

                //set name on view
                LoginUserName.Text = user.Name;

                //enable login button
                SignInButton.IsEnabled = true;
            }
            else
            {
                //if there is not user a account must be created
                LoginUserName.Text =  createAccount;

                //disable login button
                SignInButton.IsEnabled = false;
            }


            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
            //isInternetConnected = false;
            if (isInternetConnected)
            {
                //remote login 


            }
            else
            {
                // No internet 
                InternetStatus.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 50, 170, 207));
                var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");
                string message = resourceLoader.GetString("/Strings/InternetStatusFalse/Text");
                InterneStatusText.Text = message;
                //PassportSignInButton.IsEnabled = false;
            }
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
           
            user.Password = passwordBox.Password;
            Res res = await Auth.SignIn(user);
            Debug.WriteLine(res.Success);
            if (res.Success)
            {
                Frame.Navigate(typeof(MainPage));


            }
            else
            {
                ErrorMessage.Text = wrongPassword;
            }
        }
        private void AccountManagementButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ErrorMessage.Text = "";
            if (user == null)
            {
                Frame.Navigate(typeof(AccountManagementSyncOrNew));
               // Frame.Navigate(typeof(NewUserView));
            }
            else
            {
                Frame.Navigate(typeof(AccountManagementView));
            }

        }
    }
}
