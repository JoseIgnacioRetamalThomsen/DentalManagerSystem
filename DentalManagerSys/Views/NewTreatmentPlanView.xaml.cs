///------------------------------------------
///
///  Dental Manager System
///  Profesional Practice in IT project
///  GMIT 2019
///  
///  Markm Ndipenoch
///  Jose I. Retamal
///------------------------------------------
///

using DataAccessLibrary;
using DentalManagerSys.Print;
using DentalManagerSys.ViewModel;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// View for create a new treatment plan.
    /// </summary>
    public sealed partial class NewTreatmentPlanView : Page
    {
        #region Properties
        ObservableCollection<Treatment> treatments = null;
        //ObservableCollection<Treatment> treatmentsOnPlan = new ObservableCollection<Treatment>();

        public NewTreatmentPlanViewModel ViewModel { get; set; }
        public object TreatmentPanPrint { get; private set; }
        private AdminDetails ad;
        #endregion
        #region Constructors 
        public NewTreatmentPlanView()
        {
            this.InitializeComponent();

            InitTreamentsCB();

            ViewModel = new NewTreatmentPlanViewModel();

            TreatmentsDoneListView.ItemsSource = ViewModel.TreatmentsOnPlan;
            SelectToothCB.ItemsSource = ViewModel.Tooths;
            ViewModel.Total = 0;


        }
        #endregion
        #region Build
        private void InitTreamentsCB()
        {
            treatments = new ObservableCollection<Treatment>(DAO.GetAllTreatment());
            TreatmentsCB.ItemsSource = treatments;
            ad = DAO.GetAdminDetails(App.ActualUser.Email);
            Debug.WriteLine(ad.city);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {

            }
            else
            {
                ViewModel.ActualCustomer = DAO.GetCustomerByID(e.Parameter.ToString());
                PageTitle.Text = ViewModel.ActualCustomer.name + " " + ViewModel.ActualCustomer.surname;


            }

            //init print helper
            base.OnNavigatedTo(e);


        }

        #endregion

        #region buttons and events handlers 
        /// <summary>
        /// There is a treatmet selected, when hapens the treatment price can be change or the 
        /// treatment can be removed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreatmentsDoneListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListView)sender).SelectedIndex < 0) return;
            //set value of treament on edit box for editing
            EditPriceTB.Text = ((int)ViewModel.TreatmentsOnPlan[((ListView)sender).SelectedIndex].Price).ToString();
            SaveChangedPriceButton.IsEnabled = true;
            RemoveTreatmentButton.IsEnabled = true;
        }

        /// <summary>
        /// Select treatment for add.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreatmentsCB_DropDownClosed(object sender, object e)
        {
            ComboBox temp = (ComboBox)sender;
            Treatment t;
            try
            {
                t = treatments[temp.SelectedIndex];
            }
            catch
            {
                //No selection whe do nothging
                return;
            }

            ViewModel.PriceBefore = (int)t.Price;
            ViewModel.BeforeTreatment = t;
            isTreament = true;
            EnableAddButton();

        }

        /// <summary>
        /// Limit price text to only numbers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void EditPriceTB_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = new String(sender.Text.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Select tooth.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectToothCB_DropDownClosed(object sender, object e)
        {
            isToothNumber = true;
            try
            {
                ViewModel.Tooth = Convert.ToInt32(((ComboBox)sender).SelectedValue.ToString());
            }
            catch
            {
                //no selection, do nothing
                return;
            }
            EnableAddButton();
        }
        #endregion

        #region buttons
        /// <summary>
        /// Create the treatmwent plan an go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateTreatmentPlanButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.CreateNewTreatmentPlan();
            TreatmentPlanPrint tp = new TreatmentPlanPrint(ViewModel.ActualCustomer, new List<TreatmentOnPlan>(ViewModel.TreatmentsOnPlan), App.ActualUser, ad);
            Frame.Navigate(typeof(TreatmentPlanPreviewView), tp);
            // Frame.GoBack();
        }

        /// <summary>
        /// Change price of the selected treatment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChangedPriceButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            decimal NewPrice = Convert.ToDecimal(EditPriceTB.Text);
            int ItemIndex = TreatmentsDoneListView.SelectedIndex;

            //edit value
            ViewModel.TreatmentsOnPlan[ItemIndex].Price = NewPrice;
            EditDone();


        }
        private void RemoveTreatmentButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int ItemIndex = TreatmentsDoneListView.SelectedIndex;
            ViewModel.TreatmentsOnPlan.RemoveAt(ItemIndex);
            EditDone();

        }

        /// <summary>
        /// Add the treatment to plan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButon_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Comments = CommentTB.Text;
            ViewModel.AddTreatment();
            TreatmentsCB.SelectedItem = null;
            SelectToothCB.SelectedItem = null;
            isTreament = false;
            isToothNumber = false;
            AddButon.IsEnabled = false;
            CommentTB.Text = "";
            ViewModel.PriceBefore = 0;
        }

        private bool isTreament = false;
        private bool isToothNumber = false;

        /// <summary>
        /// Enable the add buton when tooth and treatement are selected
        /// </summary>
        private void EnableAddButton()
        {
            if (isTreament && isToothNumber)
            {
                AddButon.IsEnabled = true;
            }
        }

        /// <summary>
        /// Show preview print.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewTreatmentPlan_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //create treatment plan for print 
            TreatmentPlanPrint tp = new TreatmentPlanPrint(ViewModel.ActualCustomer, new List<TreatmentOnPlan>(ViewModel.TreatmentsOnPlan), App.ActualUser, ad);
            Frame.Navigate(typeof(TreatmentPlanPreviewView), tp);

        }

        private void PrintTreatmentPlan_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //create treatment plan for print 
            TreatmentPlanPrint tp = new TreatmentPlanPrint(ViewModel.ActualCustomer, new List<TreatmentOnPlan>(ViewModel.TreatmentsOnPlan), App.ActualUser, ad);
            Frame.Navigate(typeof(TreatmentPlanPreviewView), tp);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        #endregion

        #region util
        private void ReloadTretmentsListView()
        {
            TreatmentsDoneListView.SelectionChanged -= TreatmentsDoneListView_SelectionChanged;
            TreatmentsDoneListView.ItemsSource = null;
            TreatmentsDoneListView.ItemsSource = ViewModel.TreatmentsOnPlan;
            TreatmentsDoneListView.SelectionChanged += TreatmentsDoneListView_SelectionChanged;
        }

        private void EditDone()
        {
            ViewModel.RecalculateTotal();
            ReloadTretmentsListView();
            SaveChangedPriceButton.IsEnabled = false;
            RemoveTreatmentButton.IsEnabled = false;
        }
        #endregion


    }


}
