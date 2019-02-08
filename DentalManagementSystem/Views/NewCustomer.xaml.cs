using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagementSystem.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewCustomer : Page
    {
        public NewCustomer()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Clear all inputs in view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            inputName.Text = "";
            inputSurename.Text = "";
            idInput.Text = "";
            emaillInput.Text = "";
            streetInput.Text = "";
            cityInput.Text = "";
            provinceInput.Text = "";
            countryInput.Text = "";
            postCodeInput.Text = "";
            mobilNumInput.Text = "";
            homeNumInput.Text = "";
            birthDatePicker.Date = new DateTime();

        }


        private void AddButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (inputName.Text == "")
            {
                topTextBlock.Text = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings").GetString("NameError/Text");
                topTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }else if(inputSurename.Text == "")
            {
                topTextBlock.Text = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings").GetString("SurnameError/Text");
                topTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }else if (idInput.Text == "")
            {
                topTextBlock.Text = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings").GetString("IDError/Text");
                topTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
