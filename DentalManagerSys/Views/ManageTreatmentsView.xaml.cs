﻿using DataAccessLibrary;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ManageTreatmentsView : Page
    {


        ObservableCollection<Treatment> treatmentsList = null;

        public ManageTreatmentsView()
        {
            this.InitializeComponent();
            treatmentsList = new ObservableCollection<Treatment>(DAO.GetAllTreatment());

           // treatmentsList.Add(new Treatment("00", "T1", 45000));
           // treatmentsList.Add(new Treatment("00", "T2", 43000));
        }

        private void ViewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewTreatment));
        }
    }
}
