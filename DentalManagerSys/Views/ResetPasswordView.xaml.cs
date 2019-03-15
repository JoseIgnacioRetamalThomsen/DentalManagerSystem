using DataAccessLibrary.REST;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    
    public sealed partial class ResetPasswordView : Page
    {
        private string message1;
        private string message2;
        public ResetPasswordView()
        {
            this.InitializeComponent();
            InitStrings();
        }

        private void InitStrings()
        {
            message1 = "We send you a email with a link for change your password";
            message2 = "Change your password";
        }

        private async void ResetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User("name","password", Email.Text);
            await Auth.ResetPassword(user);
            await new MessageDialog(message1, message2).ShowAsync();
            Frame.Navigate(typeof(LoginView));
        }
    }
}
