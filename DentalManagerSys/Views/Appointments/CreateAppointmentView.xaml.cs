using DataAccessLibrary;
using DataAccessLibrary.REST;
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

        ApointmetsView av;
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
                //ViewModel = new CreateAppointmentViewModel();
            }
            else
            {
                ViewModel = new CreateAppointmentViewModel();
                ViewModel.SetCustomer(((NewPaymentData)e.Parameter).CustomerId);
            }

            //Customer Test = new Customer("12234543-k", "Marco Antonio", "Perez Gonzales", "03/03/1978 00:00:00", "Los leons 29",
            //    "Valpariso", "Valparaiso", "Chile", "gh567", "0983442233", "02122222", DateTime.Now, "Sin alergias");
            //  ViewModel.Customer = Test;

            av = new ApointmetsView();
            ScrollViewer sv = new ScrollViewer();
            sv.Content = av;
            SlotPickSP.Children.Add(sv);
            av.OnEmptySlotTapped += Av_OnEmptySlotTapped;

            CalDate.Date = DateTime.Now;

            ShowAppointments();

            CalDate.DateChanged += CalDate_DateChanged;

        }

        DateTime definitive;
        private void Av_OnEmptySlotTapped(object sender, EmptySlotTapped e)
        {
            //enable create appointment button
            CreateAppointmentButton.IsEnabled = true;


            //show stack panel with date
            MessageSP.Visibility = Visibility.Collapsed;
            SelectedDataSP.Visibility = Visibility.Visible;

            int slot = e.Slot;//e.Y % 4;



            int min = ApointmetsView.slot[slot];
            int hour = e.Y / 4 + 8;
            Debug.WriteLine("houe+" + hour);
            DateTime dt = CalDate.Date.Value.DateTime;

            DateTime startDay;

            int newd = 0;
            if (dt.DayOfWeek == DayOfWeek.Sunday)
            {
                startDay = dt.AddDays(-6);
            }
            else
            {
                startDay = dt.AddDays(1 - (int)dt.DayOfWeek);
            }



            DateTime selectedDay = startDay.AddDays(e.X - 1);

            definitive = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day, hour, min, 00);

            SelectedDate.Date = definitive.Date;
            SelectedTime.Time = definitive.TimeOfDay;



        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            Appointment ap = new Appointment();

            ap.Date = definitive;
            ap.PatientID = ViewModel.Customer.iD;
            ap.Status = 0;

            App.Data.AddNewAppointment(ap);
        }

        private async void ShowAppointments()
        {
            //get starting day of selected week
            DateTime dt = CalDate.Date.Value.DateTime;

            DateTime startDay;

            int newd = 0;
            if (dt.DayOfWeek == DayOfWeek.Sunday)
            {
                startDay = dt.AddDays(-6).Date + new TimeSpan(0, 0, 0);
            }
            else
            {
                startDay = dt.AddDays(1 - (int)dt.DayOfWeek).Date + new TimeSpan(0, 0, 0);
            }

            //last day date
            DateTime lastDay = startDay.AddDays(7);

            //get appointments
            List<Appointment> aps = DAO.GetAppointmetsWeek(startDay);
            List<AppointmentM> apm = await AppointmentMlab.GetAppointmentsWeek(startDay, App.ActualUser.Email);

            //display appointments


            // av.AddAppointments(aps,AppointmentStatus.Active);
            av.AddAppointments(apm, AppointmentStatus.Active);
        }

        /// <summary>
        /// Create the new appointment and navigate back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Appointment ap = new Appointment();

            ap.Date = definitive;
            ap.PatientID = ViewModel.Customer.iD;
            ap.Status = 0;

            long id = App.Data.AddNewAppointment(ap);
            ap.ID = (int)id;
            await AppointmentMlab.AddNewAppointment(ap, App.ActualUser.Email);

            // AppointmentMlab.GetAppointmentsWeek(DateTime.Now, App.ActualUser.Email);
            //Frame.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void CalDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            SlotPickSP.Children.RemoveAt(0);
            av = new ApointmetsView();
            ScrollViewer sv = new ScrollViewer();
            sv.Content = av;
            SlotPickSP.Children.Add(sv);
            av.OnEmptySlotTapped += Av_OnEmptySlotTapped;

           // CalDate.Date = DateTime.Now;

            ShowAppointments();
        }
    }
}
;
