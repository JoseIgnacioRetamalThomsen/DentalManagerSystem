using DataAccessLibrary;
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

           
        }

        private void CalDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
       
            SlotPickSP.Children.RemoveAt(0);
            av = new ApointmetsView();
            ScrollViewer sv = new ScrollViewer();
            sv.Content = av;
            SlotPickSP.Children.Add(sv);

            ShowAppointments();
        }

        private void ShowAppointments()
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

            //display appointments
          

            av.AddAppointments(aps);
        }
    }
}
