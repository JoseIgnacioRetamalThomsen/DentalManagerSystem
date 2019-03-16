using DataAccessLibrary;
using DentalManagerSys.Views;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DentalManagerSys
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            DAO.InitializeDatabase();
            FireBaseDAO f = new FireBaseDAO();
           f.ReadDataFromFirebase();

            // f.AddNewTreatment("Cannines", 2000);
            // f.UpdateCustomer("g0026", "James", "", "Ndip", "123 Monivea", "Galway", "g00352031", "g00352031", "g00352031", "g00352031", "g00352031", "g00352031", "Is Test Marshall");
            // DAO.UpdateTreatmentOnPlan(new TreatmentOnPlan(1, "text", 7));
            //f.UpdateTreatmentOnPlan(new TreatmentOnPlan(2, 4,2,40, 20080501));
            // f.AddNewpayment(1,"G00352031",8000,"25/08/2014");
            // f.AddNewTreatmentPlan("g0026", 0,"25/08/2014", "09/03/2020");
            //f.AddNewTreatmentPlan("G00352031", 1, "25/08/2012", "08/03/2012");
            //f.UpdateTreatment(new Treatment(1,"Test 6",60000));
            //f.AddNewTreatment(new Treatment(2, "Test 2", 200));
            // f.AddNewTreatment(new Treatment(8, "Test 8", 800));
            // f.UpdateTreatment(new Treatment(10, "Test 6", 60000));
            //f.UpdateTreatment(new Treatment(4, "Test 4", 4000));
            //f.UpdateTreatment(new Treatment(26, "4 Molars ", 600));





        }
        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                 ContentFrame.Navigate(typeof(SettingsView));
            }
            else
            {
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);
            }

        }//NavView_ItemInvoked(


        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // you can also add items in code behind
            //NavView.MenuItems.Add(new NavigationViewItemSeparator());
            //NavView.MenuItems.Add(new NavigationViewItem()
            //{
            //    Content = "My content",
            //    Icon = new SymbolIcon(Symbol.Folder),
            //    Tag = "content"
            //});

            // set the initial SelectedItem
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                Debug.Print(item.Tag.ToString());
                if (item is NavigationViewItem && item.Tag.ToString() == "PatientsList")
                {
                    NavView.SelectedItem = item;
                    //dont understand just this work...
                    ContentFrame.Navigate(typeof(ViewAllCustomers));

                    break;
                }
            }
        }//NavView_Loaded


        private void On_Navigated(object sender, NavigationEventArgs e)
        {
             NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(SettingsView))
            {
                NavView.SelectedItem = NavView.SettingsItem as NavigationViewItem;
            }
            else
            {
                Dictionary<Type, string> lookup = new Dictionary<Type, string>()
                {
                   {typeof(NewPaymentView), "NewPaymentView"},
                    {typeof(NewCustomer), "newCustomer"}//,
                  //  {typeof(GamesPage), "games"},
                  //  {typeof(MusicPage), "music"},
                   // {typeof(MyContentPage), "content"}
                };

                String stringTag = lookup[ContentFrame.SourcePageType];
                //Debug.Write(stringTag);
                // set the new SelectedItem
                foreach (NavigationViewItemBase item in NavView.MenuItems)
                {
                    if (item is NavigationViewItem && item.Tag.Equals(stringTag))
                    {
                        //Debug.Write("yes");
                        item.IsSelected = true;
                        break;
                    }
                }
            }

        }//On_Navigated

        private void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {


                case "PatientsList":
                    ContentFrame.Navigate(typeof(ViewAllCustomers));
                    break;
                case "ManageTreaments":
                    ContentFrame.Navigate(typeof(ManageTreatmentsView));
                    break;
                case "ViewAllTransactionsNV":
                    ContentFrame.Navigate(typeof(NewCustomer));
                    break;
                case "content":
                    ContentFrame.Navigate(typeof(MainPage));
                    break;

            }

        }// NavView_Navigate(NavigationViewItem item)

        private bool On_BackRequested()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            //// Don't go back if the nav pane is overlayed.
            //if (NavView.IsPaneOpen &&
            //    (NavView.DisplayMode == muxc.NavigationViewDisplayMode.Compact ||
            //     NavView.DisplayMode == muxc.NavigationViewDisplayMode.Minimal))
            //    return false;

            ContentFrame.GoBack();
            return true;
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }
    }
}
