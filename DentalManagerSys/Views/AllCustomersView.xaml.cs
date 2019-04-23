using DataAccessLibrary;
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
            Frame.Navigate(typeof(NewPaymentView), new NewPaymentData(((Customer)DataGrid.SelectedItem).iD, 0, false),
                 new DrillInNavigationTransitionInfo());
        }

        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null|| String.IsNullOrEmpty(sender.Text))
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


    }
}

