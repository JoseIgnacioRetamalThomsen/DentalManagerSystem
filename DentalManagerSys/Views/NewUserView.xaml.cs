using DataAccessLibrary;
using DataAccessLibrary.REST;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewUserView : Page
    {

        private string passwordError;
        private string nameError;
        private string emailErrorInUse;
        private string emailErrorBad;
        private string passWordErrorShort;
        public NewUserView()
        {
            this.InitializeComponent();

            LoadString();

        }

        private void LoadString()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");

            passwordError = resourceLoader.GetString("/Strings/passwordError/Text");
            nameError = resourceLoader.GetString("/Strings/nameError/Text");
            emailErrorInUse = resourceLoader.GetString("/Strings/emailErrorInUse/Text");
            emailErrorBad = resourceLoader.GetString("/Strings/emailErrorBad/Text");
            passWordErrorShort = resourceLoader.GetString("/Strings/passWordErrorShort/Text");
        }

        /// <summary>
        /// Add a new user from gui
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NewAccountButton_Click(object sender, RoutedEventArgs e)
        {

            //check passwords match
            if (!newPasswordBox.Password.Equals(confirmPasswordBox.Password))
            {
                ErrorMessage.Text = passwordError;
            }
            else
            {
                //check user name lenght
                if (userNameInput.Text.Length < 2)
                {
                    ErrorMessage.Text = nameError;
                }
                else
                {
                    ErrorMessage.Text = "";
                    //Http request with created user
                    var user = new User { Name = userNameInput.Text, Password = newPasswordBox.Password, Email = Email.Text };
                    Res res = await Auth.AddNewUser(user);

                    //res succes new user created
                    if (res.Success)
                    {
                        //add user to local DB
                        DAO.AddNewUser(new User(userNameInput.Text, newPasswordBox.Password, Email.Text));

                        //navigate to login page
                        Frame.Navigate(typeof(LoginView));

                    }
                    else
                    {
                        //user not created, server don't allow.
                        //Show error messages
                        switch (res.Code)
                        {
                            case "auth/invalid-email":
                                ErrorMessage.Text = emailErrorBad;
                                break;
                            case "auth/email-already-exists":
                                ErrorMessage.Text = emailErrorInUse;
                                break;
                            case "auth/invalid-password":
                                ErrorMessage.Text = passWordErrorShort;
                                break;
                        }
                    }
                }

            }

        }
    }
}
