using DataAccessLibrary;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewAllCustomers : Page
    {
        Customer customer;
        Customer customer1;
        ObservableCollection<Customer> customerList = null;
        public ViewAllCustomers()
        {
            this.InitializeComponent();
            customerList = new ObservableCollection<Customer>(DAO.GetAllCustomer());

            /*
            customer = new Customer("G000352030", "markxcvxczvxczvzxcvxcz", "ndipenochvbcxzvbxcvxcxcvz", "2899-23-12", "1 Dublin Road", "Galway", "Connaught", "Ireland", "HP1009", "008795623", "00000", new DateTime(),"Is Test Marshall");
            customer1 = new Customer("G0003520302", "markxcvxczvxczvzxcvxcz", "ndipenochvbcxzvbxcvxcxcvz", "2899-23-12", "1 Dublin Road", "Galway", "Connaught", "Ireland", "HP1009", "008795623", "00000", new DateTime(),"Is Test Marshall");


            customerList.Add(customer);
            customerList.Add(customer1);*/
            CustomersListView.ItemsSource = customerList;

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
            Debug.WriteLine("change");
        }
    }
}

