﻿using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagementSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewTreatment : Page
    {
        public NewTreatment()
        {
            this.InitializeComponent();
        }

        private void ClearButton_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void AddButton_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
        /// <summary>
        /// Allow only number in Price input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void inputTreatmentPrice_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = new String(sender.Text.Where(char.IsDigit).ToArray());
        }

        
    }
}
