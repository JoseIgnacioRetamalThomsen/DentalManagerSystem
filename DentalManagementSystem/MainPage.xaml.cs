using System;
using System.Collections.Generic;
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
using DataAccessLibrary;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DentalManagementSystem
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
            DAO.AddNewCustomer("G000352030", "mark", "ndipenoch","2899-23-12", "1 Dublin Road", "Galway", "Connaught", "Ireland", "HP1009", "008795623", "00000", "G00352031@GMIT.IE");
            DAO.AddNewTreatment("Remove molar tooth",2500);
            DAO.AddNewTreatmentPlan("G000352030","OPEN", "2018-02-08", "2019-02-08");
            DAO.AddNewTreatmentPlanTreatments(1,1,2000, "2018-02-08");


            //add string resource
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");
            TextBlock textBlock = new TextBlock();
          //  Debug.Write("\n here " + resourceLoader.GetString("Title/Text"));
           

        }
    }
}
