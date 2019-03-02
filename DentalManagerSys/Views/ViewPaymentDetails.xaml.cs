///------------------------------------------
///
///  Dental Manager System
///  Profesional Practice in IT project
///  GMIT 2019
///  
///  Markm Ndpenoch
///  Jose I. Retamal
///------------------------------------------
///

using DataAccessLibrary;
using Models;
using Windows.UI.Xaml.Controls;

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewPaymentDetails : Page
    {
        private string iD;


        public ViewPaymentDetails()
        {
            this.InitializeComponent();
        }

        private void DisplayPyamentDetails(string id)
        {

            Payments temp = DAO.GetPaymenyByCustomerID(id);

            customerID.Text = temp.customerID;

        }
    }
}
