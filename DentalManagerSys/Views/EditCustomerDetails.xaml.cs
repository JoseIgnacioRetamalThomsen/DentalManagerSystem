using DataAccessLibrary;
using Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditCustomerDetails : Page
    {
        private string iD;
        public EditCustomerDetails()
        {
            this.InitializeComponent();

        }
        Customer temp;
        private void DisplayDetails(string iD)
        {

            temp = DAO.GetCustomerByID(iD);

            // PageTitle.Text = temp.name + " " + temp.surname;
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


        private void SaveCustomerDetails_Click(object sender, RoutedEventArgs e)
        {
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
            );
            Frame.GoBack();
        }

        private void CancelEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
