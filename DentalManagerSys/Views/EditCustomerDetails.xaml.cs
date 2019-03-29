using DataAccessLibrary;
using Models;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditCustomerDetails : Page
    {
        /// <summary>
        /// Initial customer data
        /// </summary>
        Customer temp;
        /// <summary>
        /// Create page
        /// </summary>
        public EditCustomerDetails()
        {
            this.InitializeComponent();

        }

        /// <summary>
        /// Load customer data
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {

            }
            else
            {
                DisplayDetails(e.Parameter.ToString());
            }

        }

        /// <summary>
        /// Show customer data in view
        /// </summary>
        /// <param name="iD"></param>
        private void DisplayDetails(string iD)
        {

            temp = DAO.GetCustomerByID(iD);

            NameTextBox.Text = temp.name;
            SurnameTextBox.Text = temp.surname;
            DOBTextBox.Text = temp.dOB.ToString();
            streetTextBox.Text = temp.street;
            cityTextBox.Text = temp.city;
            provinceTextBox.Text = temp.province;
            countryTextBox.Text = temp.country;
            postcodeTextBox.Text = temp.postcode;
            mobileTextBox.Text = temp.mobileNum;
            fixTextBox.Text = temp.homeNum;
            emailTextBox.Text = temp.email;
            commentsTextBox.Text = temp.comments;
        }
        
        /// <summary>
        /// Save customer data and go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCustomerDetails_Click(object sender, RoutedEventArgs e)
        {
            App.Data.UpdateCustomerDetails(
                temp.iD,
                NameTextBox.Text,
                SurnameTextBox.Text,
                DOBTextBox.Text,
                streetTextBox.Text,
                cityTextBox.Text,
                provinceTextBox.Text,
                countryTextBox.Text,
                postcodeTextBox.Text,
                mobileTextBox.Text,
                fixTextBox.Text,
                emailTextBox.Text,
                commentsTextBox.Text
                );

           /* FireBaseDAO f = new FireBaseDAO();
            f.UpdateCustomer(
                   temp.iD,
                NameTextBox.Text,
                SurnameTextBox.Text,
                DOBTextBox.Text,
                streetTextBox.Text,
                cityTextBox.Text,
                provinceTextBox.Text,
                countryTextBox.Text,
                postcodeTextBox.Text,
                mobileTextBox.Text,
                fixTextBox.Text,
                emailTextBox.Text,
                commentsTextBox.Text
                );

            DAO.UpdateCustomer(
                temp.iD,
                NameTextBox.Text,
                SurnameTextBox.Text,
                DOBTextBox.Text,
                streetTextBox.Text,
                cityTextBox.Text,
                provinceTextBox.Text,
                countryTextBox.Text,
                postcodeTextBox.Text,
                mobileTextBox.Text,
                fixTextBox.Text,
                emailTextBox.Text,
                commentsTextBox.Text
            );*/
            Frame.GoBack();
        }

        /// <summary>
        /// Go back with no save data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
