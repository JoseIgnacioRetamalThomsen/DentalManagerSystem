using DentalManagerSys.ViewModel;
using Models;
using System;
using System.Collections.Generic;
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
    public sealed partial class TreatmentPlanView : Page
    {
        TreatmentPlanViewModel ViewModel { get; set; }

        public TreatmentPlanView()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


            ViewModel = new TreatmentPlanViewModel();
            

            if (e.Parameter == null)
            {

            }
            else
            {
                ViewModel.ActualTreatmentPlan = (TreatmentPlan)e.Parameter;
            }

            Debug.WriteLine(ViewModel.ActualTreatmentPlan.CustomerID);

            //load treatments on plan
            ViewModel.LoadTreatments(ViewModel.ActualTreatmentPlan.TreatmentPLanID);
            //treatment state combo box
            TreatmentStateCB.ItemsSource = ViewModel.TreatmentsPlans;
            TreatmentStateCB.SelectedItem = ViewModel.ActualTreatmentPlanState;

            //treatments lv
            TreatmentsOnPlanLV.ItemsSource = ViewModel.TreatmentsOnPlan;
        }

        private void TreatmentStateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ActualTreatmentPlanState = (TreatmentPlaneState)((ComboBox)sender).SelectedItem;
        }

        private void TreatmentsOnPlanLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected item index
            int index = TreatmentsOnPlanLV.SelectedIndex;

            //get treatement on plan
            TreatmentOnPlan top = ViewModel.TreatmentsOnPlan[index];

            //activate button for mark as done if is not done
            if(!top.IsDone)
            {
                CreateTreatmentPlanCompletedB.IsEnabled = true;
            }else
            {
                CreateTreatmentPlanCompletedB.IsEnabled = false;
            }

        }
    }
}
