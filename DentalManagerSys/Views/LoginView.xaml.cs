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
        public LoginView()
        {
            this.InitializeComponent();
            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
            Debug.WriteLine(isInternetConnected);

        }


        protected override  void OnNavigatedTo(NavigationEventArgs e)
        {
            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
            isInternetConnected = false;
            if (isInternetConnected)
            {
            }
            else
            {
                // Microsoft Passport is not setup so inform the user
                InternetStatus.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 50, 170, 207));
                var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");
                string message = resourceLoader.GetString("/Strings/InternetStatusFalse/Text");
                InterneStatusText.Text = message;
                //PassportSignInButton.IsEnabled = false;
            }
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void AccountManagementButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ErrorMessage.Text = "";
            Frame.Navigate(typeof(AccountManagementView));
        }
    }
}
