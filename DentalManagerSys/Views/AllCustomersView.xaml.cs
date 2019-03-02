using DataAccessLibrary;
using Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewAllCustomers : Page
    {
        
        ObservableCollection<Customer> customerList = null;
        
        public ViewAllCustomers()
        {
            this.InitializeComponent();
            customerList = new ObservableCollection<Customer>(DAO.GetAllCustomer());

           
        }

      
        private void SelectCustomer_Button(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(((Customer)DataGrid.SelectedItem).iD);
            Frame.Navigate(typeof(ViewCustomerDetails), ((Customer)DataGrid.SelectedItem).iD,
                    new DrillInNavigationTransitionInfo());
        }

        private void PatientsCommandBar_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewCustomer));
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewCustomerButton.IsEnabled = true;
            NewPaymentButton.IsEnabled = true;
            EditBarButton.IsEnabled = true;
        }

        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            
            Frame.Navigate(typeof(EditCustomerDetails), ((Customer)DataGrid.SelectedItem).iD,
                    new DrillInNavigationTransitionInfo());
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewPaymentView), new NewPaymentData(((Customer)DataGrid.SelectedItem).iD,0,false) ,
                 new DrillInNavigationTransitionInfo());
        }
    }
}

