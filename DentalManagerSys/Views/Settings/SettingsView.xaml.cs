﻿using DentalManagerSys.Views.Settings;
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
    public sealed partial class SettingsView : Page
    {
        public SettingsView()
        {
            this.InitializeComponent();

            RequestLoginTS.IsOn = App.AppLocalSettings.IsLoginRequerid;
            RequestLoginTS.Toggled += RequestLoginTS_Toggled;

            BackupTS.IsOn = App.AppLocalSettings.IsBackup;
            BackupTS.Toggled += WorkOfflineTS_Toggled;

        }

     

        private void AddminAddressButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminAddress));
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ResetPasswordView));
        }


        private void RequestLoginTS_Toggled(object sender, RoutedEventArgs e)
        {
            App.AppLocalSettings.ChangeIsLoginRequerid();
        }

        private void WorkOfflineTS_Toggled(object sender, RoutedEventArgs e)
        {
            App.Data.IsWorkOffline = App.AppLocalSettings.ChangeIsBackup();
        }

        private void FeedBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FeedbackFormView));
        }
    }
}
