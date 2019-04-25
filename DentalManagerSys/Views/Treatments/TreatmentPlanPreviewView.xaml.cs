using DataAccessLibrary.REST;
using DentalManagerSys.Print;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Printing;
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
    public sealed partial class TreatmentPlanPreviewView : Page
    {
        TreatmentPlanPrint tp;

        PrintHelper ph;
        public TreatmentPlanPreviewView()
        {
            this.InitializeComponent();
            InitStaticString();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            tp = (TreatmentPlanPrint)e.Parameter;

            ph = new PrintHelper(tp);

            ph.RegisterForPrinting();

            MainContainer.Children.Add(tp);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (ph != null)
            {
                ph.UnregisterForPrinting();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private string npTittle;
        private string npContent;
        private string npOKButton;
        private string nsTitle;
        private string nsContent;

        private string TreatmentPlanEmailHeader;

        private void InitStaticString()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");
            npTittle = resourceLoader.GetString("/Strings/npTittle/Text");
            npContent=  resourceLoader.GetString("/Strings/npContent/Text");
            npOKButton = resourceLoader.GetString("/Strings/npOKButton/Text");
            nsTitle = resourceLoader.GetString("/Strings/nsTitle/Text");
            nsContent = resourceLoader.GetString("/Strings/nsContent/Text");
          
            TreatmentPlanEmailHeader = resourceLoader.GetString("/Strings/TreatmentPlanEmailHeader/Text");
        }

        private async void Print_Click(object sender, RoutedEventArgs e)
        {
            if (PrintManager.IsSupported())
            {
                try
                {
                    // Show print UI
                    await PrintManager.ShowPrintUIAsync();
                    
                }
                catch
                {
                    // Printing cannot proceed at this time
                    ContentDialog noPrintingDialog = new ContentDialog()
                    {
                        Title = npTittle,
                        Content = npContent,
                        PrimaryButtonText = npOKButton
                    };
                    await noPrintingDialog.ShowAsync();
                }
            }
            else
            {
                // Printing is not supported on this device
                ContentDialog noPrintingDialog = new ContentDialog()
                {
                    Title = nsTitle,
                    Content = nsContent,
                    PrimaryButtonText = npOKButton
                };

                await noPrintingDialog.ShowAsync();
            }

            Frame.Navigate(typeof(ViewCustomerDetails), tp.customer.iD,
                  new DrillInNavigationTransitionInfo());
        }

        private async void Email_Click(object sender, RoutedEventArgs e)
        {
            string customerEmail = tp.customer.email;
            EmailData id = new EmailData(customerEmail, App.ActualUser.Email, TreatmentPlanEmailHeader, tp.GetStringEmail());
            
            await  Emailer.SendEmail(id);

            Frame.Navigate(typeof(ViewCustomerDetails), tp.customer.iD,
                  new DrillInNavigationTransitionInfo());
        }

        /// <summary>
        /// Prin and email treatment plan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PrintAndEmail_Click(object sender, RoutedEventArgs e)
        {
            if (PrintManager.IsSupported())
            {
                try
                {
                    // Show print UI
                    await PrintManager.ShowPrintUIAsync();

                }
                catch
                {
                    // Printing cannot proceed at this time
                    ContentDialog noPrintingDialog = new ContentDialog()
                    {
                        Title = npTittle,
                        Content = npContent,
                        PrimaryButtonText = npOKButton
                    };
                    await noPrintingDialog.ShowAsync();
                }
            }
            else
            {
                // Printing is not supported on this device
                ContentDialog noPrintingDialog = new ContentDialog()
                {
                    Title = nsTitle,
                    Content = nsContent,
                    PrimaryButtonText = npOKButton
                };

                await noPrintingDialog.ShowAsync();
            }

            //email
            string customerEmail = tp.customer.email;
            EmailData id = new EmailData(customerEmail, App.ActualUser.Email, TreatmentPlanEmailHeader, tp.GetStringEmail());

            await Emailer.SendEmail(id);

            Frame.Navigate(typeof(ViewCustomerDetails), tp.customer.iD,
                    new DrillInNavigationTransitionInfo());
        }
    }
}
