using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Models;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class NewTreatment : Page
    {
        public NewTreatment()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Clear all inputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            inputTreatmentName.Text = "";
            inputTreatmentPrice.Text = "";
        }

        /// <summary>
        /// Add new treatmet to DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void AddButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AddTreatment();
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

        private void AddTreatment()
        {
            //check for both inputs
            if (inputTreatmentName.Text == "" || inputTreatmentPrice.Text == "")
            {
                topTextBlock.Text = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings").GetString("TreatmentInputError/Text");
                topTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                Treatment treatment = new Treatment(0,inputTreatmentName.Text, Convert.ToDecimal(inputTreatmentPrice.Text));
                App.Data.AddNewTreatment(treatment);

                this.ClearButton_Tapped(this, new TappedRoutedEventArgs());
            }
        }
        private void ClearInputs()
        {
            inputTreatmentName.Text = "";
            inputTreatmentPrice.Text = "";
        }
        private void AddTreatmentButton_Click(object sender, RoutedEventArgs e)
        {
            AddTreatment();
            Frame.GoBack();
        }

        private void AddTreatmentPlusButton_Click(object sender, RoutedEventArgs e)
        {
            AddTreatment();
        }

        private void RefreshTreatmentsButton_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }
    }
}
