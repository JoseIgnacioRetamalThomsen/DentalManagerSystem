using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using DataAccessLibrary;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewCustomer : Page
    {
        public NewCustomer()
        {
            this.InitializeComponent();
            DAO.InitializeDatabase();
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
            CommentsInput.Text = "";

        }


        /// <summary>
        /// Get Data from view and if name, surname and id are there create new customer, if any of those values is missing prompt error message or
        ///  if user Id is not unique will prompt error message as well.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //check for required values
            if (inputName.Text == "")
            {
                topTextBlock.Text = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings").GetString("NameError/Text");
                topTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }
            else if (inputSurename.Text == "")
            {
                topTextBlock.Text = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings").GetString("SurnameError/Text");
                topTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }
            else if (idInput.Text == "")
            {
                topTextBlock.Text = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings").GetString("IDError/Text");
                topTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                //create new customer 
                Debug.WriteLine(birthDatePicker.Date.ToString("yyyy-MM-dd"));


                //check if added, if not unique id
                bool temp = DAO.AddNewCustomer(
                    idInput.Text, //id
                    inputName.Text, //name
                    inputSurename.Text,//surname
                    birthDatePicker.Date.ToString("yyyy-MM-dd"), //dob
                    streetInput.Text, //street address
                    cityInput.Text, //city
                    provinceInput.Text,//Province
                    countryInput.Text, //country
                    postCodeInput.Text,//postcode
                    mobilNumInput.Text,//mobil number
                    homeNumInput.Text,//home number
                    emaillInput.Text, //email
                    CommentsInput.Text//comments
                    );
                Debug.Write(temp);
                if (temp)
                {
                    //all good was created
                    ClearButton_Tapped(this, new TappedRoutedEventArgs());
                    ResetMessageText();
                }
                else
                {
                    topTextBlock.Text = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings").GetString("UniqueIdError/Text");
                    topTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
        }

        private void ResetMessageText()
        {
            topTextBlock.Text = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings").GetString("NewCustomerTextBlock/Text");
        }
    }
}
