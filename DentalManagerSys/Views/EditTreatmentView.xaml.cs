using DentalManagerSys.ViewModel;
using Models;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditTreatmentView : Page
    {

        public EditTreatmentViewModel ViewModel { get; set; }
        /// <summary>
        /// Create page
        /// </summary>
        public EditTreatmentView()
        {
            this.InitializeComponent();
        }


        /// <summary>
        /// Call when navigate to page, initializate ViewModel
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {

            }
            else
            {
                ViewModel = new EditTreatmentViewModel((Treatment)(e.Parameter));

            }

        }
        /// <summary>
        /// Allow only number in Price input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void inputTreatmentPrice_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = new String(sender.Text.Where(char.IsDigit).ToArray());
        }


        /// <summary>
        /// Save the changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTreatmentButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveTreatment(inputTreatmentName.Text, Convert.ToDecimal(inputTreatmentPrice.Text));
            Frame.GoBack();
        }

        /// <summary>
        /// Back with no changes save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelTreatmentChangesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
