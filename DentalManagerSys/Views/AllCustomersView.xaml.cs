using DataAccessLibrary;
using DentalManagerSys.Views.Appointments;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace DentalManagerSys.Views
{
    /// <summary>
    /// Main View, shows all customer(patients)
    /// </summary>
    public sealed partial class ViewAllCustomers : Page
    {
        #region Properties
        /// <summary>
        /// List of customer to fill from database
        /// </summary>
        ObservableCollection<Customer> customerList = null;
        #endregion
        #region Constructors
        /// <summary>
        /// Create view, populate customerList array
        /// </summary>
        public ViewAllCustomers()
        {
            this.InitializeComponent();

            customerList = new ObservableCollection<Customer>(DAO.GetAllCustomer());

        }
        #endregion
        #region Navbar buttons
        private void SelectCustomer_Button(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewCustomerDetails), ((Customer)DataGrid.SelectedItem).iD,
                    new DrillInNavigationTransitionInfo());
        }


        private void AddNewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewCustomer));
        }

        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(EditCustomerDetails), ((Customer)DataGrid.SelectedItem).iD,
                    new DrillInNavigationTransitionInfo());
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewPaymentView), new NewPaymentData(((Customer)DataGrid.SelectedItem).iD, 0, false),
                 new DrillInNavigationTransitionInfo());
        }

        private void NewAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateAppointmentView), new NewPaymentData(((Customer)DataGrid.SelectedItem).iD, 0, false),
               new DrillInNavigationTransitionInfo());
        }
        #endregion
        #region ListView
        /// <summary>
        /// Make buttons for selected customer availble.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewCustomerButton.IsEnabled = true;
            NewPaymentButton.IsEnabled = true;
            EditBarButton.IsEnabled = true;
            NewAppointmentButton.IsEnabled = true;
        }
        #endregion
        #region Search Box
        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null || String.IsNullOrEmpty(sender.Text))
            {
                Debug.WriteLine("eser");
                customerList.Clear();
                customerList = new ObservableCollection<Customer>(DAO.GetAllCustomer());
                DataGrid.ItemsSource = customerList;
            }
            else
            {
                CultureInfo culture = CultureInfo.CurrentCulture;

                string[] searchFor = sender.Text.Split(new char[] { ' ' });

                ObservableCollection<Customer> searchResult = new ObservableCollection<Customer>();
                foreach (Customer customer in customerList)
                {
                    foreach (string searchpar in searchFor)
                    {
                        if (culture.CompareInfo.IndexOf(customer.name, searchpar, CompareOptions.IgnoreCase) >= 0 || culture.CompareInfo.IndexOf(customer.surname, searchpar, CompareOptions.IgnoreCase) >= 0)
                        {
                            searchResult.Add(customer);
                        }
                    }

                }
                customerList.Clear();
                customerList = searchResult;
                DataGrid.ItemsSource = customerList;
            }
        }
        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                string[] searchFor = sender.Text.Split(new char[] { ' ' });

                ObservableCollection<Customer> searchResult = new ObservableCollection<Customer>();
                foreach (Customer customer in customerList)
                {
                    foreach (string searchpar in searchFor)
                    {
                        if (culture.CompareInfo.IndexOf(customer.name, searchpar, CompareOptions.IgnoreCase) >= 0 || culture.CompareInfo.IndexOf(customer.surname, searchpar, CompareOptions.IgnoreCase) >= 0)
                        {
                            searchResult.Add(customer);
                        }
                    }
                }
                sender.ItemsSource = searchResult;
            }
        }

        private void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            string id = ((Customer)args.SelectedItem).iD;
            Debug.WriteLine(args.SelectedItem);
            Frame.Navigate(typeof(ViewCustomerDetails), ((Customer)args.SelectedItem).iD,
                   new DrillInNavigationTransitionInfo());
        }
        #endregion
    }
}

