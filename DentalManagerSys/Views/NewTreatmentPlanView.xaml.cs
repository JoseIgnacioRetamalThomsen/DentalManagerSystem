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
        ObservableCollection<Treatment> treatmentsOnPlan = new ObservableCollection<Treatment>();
        

        public NewTreatmentPlanView()
        {
            this.InitializeComponent();

            InitTreamentsCB();

            TreatmentsDoneListView.ItemsSource = treatmentsOnPlan;
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
            EditPriceTB.Text = treatmentsOnPlan[((ListView)sender).SelectedIndex].price.ToString();
        }

        private void TreatmentsCB_DropDownClosed(object sender, object e)
        {
            ComboBox temp = (ComboBox)sender;
            Treatment t = treatments[temp.SelectedIndex];
            treatmentsOnPlan.Add(new Treatment(t.iD, t.name, t.price));
        }

        private void EditPriceTB_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = new String(sender.Text.Where(char.IsDigit).ToArray());
        }
    }


}
