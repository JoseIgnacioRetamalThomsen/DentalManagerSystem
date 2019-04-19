using DentalManagerSys.ViewModel;
using DentalManagerSys.Views.Form;
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

namespace DentalManagerSys.Views.Appointments
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateAppointmentView : Page
    {
        public CreateAppointmentViewModel ViewModel;
        public CreateAppointmentView()
        {
            this.InitializeComponent();
            
        }

        /// <summary>
        /// Call on navigated , initializate the view and set sources for list views
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                ViewModel = new CreateAppointmentViewModel();
            }
            else
            {
                ViewModel = new CreateAppointmentViewModel();
                ViewModel.SetCustomer(e.Parameter.ToString());
            }

            Customer Test = new Customer("12234543-k", "Marco Antonio", "Perez Gonzales", "03/03/1978 00:00:00", "Los leons 29",
                "Valpariso", "Valparaiso", "Chile", "gh567", "0983442233", "02122222", DateTime.Now, "Sin alergias");
            ViewModel.Customer = Test;

            ApointmetsView av = new ApointmetsView();
            ScrollViewer sv = new ScrollViewer();
            sv.Content = av;
            SlotPickSP.Children.Add(sv);
            av.OnEmptySlotTapped += Av_OnEmptySlotTapped;

            CalDate.Date = DateTime.Now;

        }

        DateTime definitive;
        private void Av_OnEmptySlotTapped(object sender, EmptySlotTapped e)
        {

            Debug.WriteLine("tapped" + e.X + e.Y);
            int slot = e.Y % 4;
           // int time = e.X / 11;
            Debug.WriteLine("ex"+e.X);
            Debug.WriteLine(ApointmetsView.slot[slot]);
            int min = ApointmetsView.slot[slot];
            int hour = e.Y / 4 +8;
            Debug.WriteLine("houe+" +hour);
            DateTime dt = CalDate.Date.Value.DateTime;

            DateTime startDay;

            int newd = 0;
            if (dt.DayOfWeek == DayOfWeek.Sunday)
            {
                startDay = dt.AddDays(-6);
            }
            else
            {
                startDay = dt.AddDays(1- (int)dt.DayOfWeek);
            }
          
            
            Debug.WriteLine("last " + startDay);
            DateTime selectedDay = startDay.AddDays(e.X-1);
            Debug.WriteLine("last " + selectedDay);
             definitive = new DateTime(selectedDay.Year,selectedDay.Month,selectedDay.Day,hour,min,00);
            Debug.WriteLine(definitive);
            SelectedDate.Date = definitive.Date;
            SelectedTime.Time = definitive.TimeOfDay;



        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            Appointment ap = new Appointment();

            ap.Date = definitive;
            ap.PatientID = 1;
            ap.Status = 0;

            App.Data.AddNewAppointment(ap);
        }
    }
}
;