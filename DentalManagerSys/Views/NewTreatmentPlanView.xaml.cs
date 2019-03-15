﻿using DataAccessLibrary;
using DentalManagerSys.ViewModel;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewTreatmentPlanView : Page
    {

        ObservableCollection<Treatment> treatments = null;
        //ObservableCollection<Treatment> treatmentsOnPlan = new ObservableCollection<Treatment>();

        public NewTreatmentPlanViewModel ViewModel { get; set; }
        public NewTreatmentPlanView()
        {
            this.InitializeComponent();

            InitTreamentsCB();

            ViewModel = new NewTreatmentPlanViewModel();

            TreatmentsDoneListView.ItemsSource = ViewModel.TreatmentsOnPlan;
            SelectToothCB.ItemsSource = ViewModel.Tooths;
            ViewModel.Total = 0;
        }

        private void InitTreamentsCB()
        {
            treatments = new ObservableCollection<Treatment>(DAO.GetAllTreatment());
            TreatmentsCB.ItemsSource = treatments;
            //TreatmentsCB.SelectedIndex = 0;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {

            }
            else
            {
                Debug.WriteLine(e.Parameter);
                //DisplayDetails(e.Parameter.ToString());
                //iD = e.Parameter.ToString();
                ViewModel.ActualCustomer = DAO.GetCustomerByID(e.Parameter.ToString());
                PageTitle.Text = ViewModel.ActualCustomer.name + " " + ViewModel.ActualCustomer.surname;


            }


        }

        private void TreatmentsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TreatmentsDoneListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void TreatmentsDoneListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("willThis" + ((ListView)sender).SelectedIndex);
            Debug.WriteLine("willThis" + ViewModel.TreatmentsOnPlan[((ListView)sender).SelectedIndex].Price.ToString());

            //set value of treament on edit box for editing
            EditPriceTB.Text = ((int)ViewModel.TreatmentsOnPlan[((ListView)sender).SelectedIndex].Price).ToString();
            SaveChangedPriceButton.IsEnabled = true;


        }

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
            //add adddes treatments to list of all treatements in plan
            //ViewModel.TreatmentsOnPlan.Add(new Treatment(t.iD, t.name, t.price));

            ViewModel.PriceBefore = t._Price;
            ViewModel.BeforeTreatment = t;
            isPrice = true;
            EnableAddButton();
            //add price of treatment to total
            // ViewModel.Total += t.Price;

        }

        private void EditPriceTB_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = new String(sender.Text.Where(char.IsDigit).ToArray());
        }



        private void CreateTreatmentPlanButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.CreateNewTreatmentPlan();
            Frame.GoBack();
        }

        private void SaveChangedPriceButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            decimal NewPrice = Convert.ToDecimal(EditPriceTB.Text);
            int ItemIndex = TreatmentsDoneListView.SelectedIndex;

            //edit value
            ViewModel.TreatmentsOnPlan[ItemIndex].Price = NewPrice;
            ViewModel.RecalculateTotal();
            ReloadTretmentsListView();
            SaveChangedPriceButton.IsEnabled = false;

        }

        private void ReloadTretmentsListView()
        {
            TreatmentsDoneListView.SelectionChanged -= TreatmentsDoneListView_SelectionChanged;
            TreatmentsDoneListView.ItemsSource = null;
            TreatmentsDoneListView.ItemsSource = ViewModel.TreatmentsOnPlan;
            TreatmentsDoneListView.SelectionChanged += TreatmentsDoneListView_SelectionChanged;
        }

        private void AddButon_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.Comments = CommentTB.Text;

            ViewModel.AddTreatment();

            TreatmentsCB.SelectedItem = null;
            SelectToothCB.SelectedItem = null;
            isPrice = false;
            isToothNumber = false;
            AddButon.IsEnabled = false;
            CommentTB.Text = "";
            ViewModel.PriceBefore = 0;
        }

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

        bool isPrice = false;
        bool isToothNumber = false;
        private void EnableAddButton()
        {
            if (isPrice && isToothNumber)
            {
                AddButon.IsEnabled = true;
            }
        }

        private void NewPaymentButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }


}
