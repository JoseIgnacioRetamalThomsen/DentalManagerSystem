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
using Windows.UI.Xaml;
using DataAccessLibrary;
using DataAccessLibrary.REST;
using Models;
using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    ///View for create a new user
    /// </summary>
    public sealed partial class NewUserView : Page
    {

        private string passwordError;
        private string nameError;
        private string emailErrorInUse;
        private string emailErrorBad;
        private string passWordErrorShort;

        /// <summary>
        /// Constructor
        /// </summary>
        public NewUserView()
        {
            this.InitializeComponent();

            LoadString();

        }

        /// <summary>
        /// Initialize string from resources.
        /// </summary>
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

        /// <summary>
        /// Go back to revius view: AccountManagement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
