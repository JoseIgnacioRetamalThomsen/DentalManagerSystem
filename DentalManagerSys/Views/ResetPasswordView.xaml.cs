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
using DataAccessLibrary.REST;
using Models;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// View for password reset.
    /// </summary>
    
    public sealed partial class ResetPasswordView : Page
    {
        private string message1;
        private string message2;

        /// <summary>
        /// Create object.
        /// </summary>
        public ResetPasswordView()
        {
            this.InitializeComponent();
            InitStrings();
        }

        /// <summary>
        /// Initializa strings from resources.
        /// </summary>
        private void InitStrings()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");

            message1 = resourceLoader.GetString("/Strings/message1/Text");
            message2 = resourceLoader.GetString("/Strings/message2/Text");
          
        }

        /// <summary>
        /// Reset the password and show message dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ResetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User("name","password", Email.Text);
            await Auth.ResetPassword(user);
            await new MessageDialog(message1, message2).ShowAsync();
            Frame.Navigate(typeof(LoginView));
        }

        /// <summary>
        /// Navigate back.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
