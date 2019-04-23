using DataAccessLibrary;
using DataAccessLibrary.REST;
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
    public sealed partial class AllAppointmentsView : Page
    {
        ApointmetsView av;
        public AllAppointmentsView()
        {
            this.InitializeComponent();

            //add appointment view
             av = new ApointmetsView();
            ScrollViewer sv = new ScrollViewer();
            sv.Content = av;
            SlotPickSP.Children.Add(sv);

            // Set date to today
            CalDate.Date = DateTime.Now;
            CalDate.DateChanged += CalDate_DateChanged;

            ShowAppointments();

            //add on clicl to av
            av.OnUsedSlotTapped += Av_OnUsedSlotTapped;


        }

        Appointment selectedAppointment;
        Customer selectedCustomerApp;
        private async void Av_OnUsedSlotTapped(object sender, SlotWithAppointmetTappedEvent e)
        {
            av.CleartHighLighted();
            av.HighLightSlot(e.X, e.Y);

            //selectedAppointment = DAO.GetAppointmentByID(e.AppointmentID);

            AppointmentM appointment = await AppointmentMlab.GetAppointmentByID(e.AppointmentID,App.ActualUser.Email);

            selectedCustomerApp = DAO.GetCustomerByID(appointment.PatientID);
            selectedCustomerApp.Print();



            NameLabel.Text = selectedCustomerApp.name + " " + selectedCustomerApp.surname;
            DateLabel.Text = appointment.Date.ToString("dddd, dd MMMM yyyy HH:mm:ss");

        }

        private void Av_OnUsedSlotTapped(object sender, EmptySlotTapped e)
        {
            av.CleartHighLighted();
            av.HighLightSlot(e.X, e.Y);
        }

        private void CalDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {

            ReloadView();


        }

        private void ReloadView()
        {
            SlotPickSP.Children.RemoveAt(0);
            av = new ApointmetsView();
            ScrollViewer sv = new ScrollViewer();
            sv.Content = av;
            SlotPickSP.Children.Add(sv);

            //ad tap event
            av.OnUsedSlotTapped += Av_OnUsedSlotTapped;

            ShowAppointments();
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
           // List<Appointment> aps = DAO.GetAppointmetsWeek(startDay);
            List<AppointmentM> apm = await AppointmentMlab.GetAppointmentsWeek(startDay,App.ActualUser.Email);

            //display appointments


            //av.AddAppointments(aps,0);
            av.AddAppointments(apm,0);
        }

        private void DeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            selectedAppointment.Status = AppointmentStatus.Cancel;
            DAO.UpdateAppointment(selectedAppointment);


            ReloadView();


        }
    }
}
