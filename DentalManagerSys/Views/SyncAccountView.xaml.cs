using DataAccessLibrary;
using DataAccessLibrary.REST;
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
    public sealed partial class SyncAccountView : Page
    {
        private string errorMessageSyncAccount = "Wrong password or account.";
        public SyncAccountView()
        {
            this.InitializeComponent();
        }

        private async void SyncAccountButton_Click(object sender, RoutedEventArgs e)
        {
           
            var user = new User("no name", PasswordBox.Password, Email.Text);
            Res res = await Auth.SignIn(user);
            if(res.Success)
            {

                UserRes userData = await Auth.GetUser(user);
                Debug.WriteLine(userData.DisplayName);
                //create user 

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
    }
}
