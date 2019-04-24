using DataAccessLibrary;
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
    public sealed partial class IncomeView : Page
    {
        string selectedDate;
        string formatedDate;
        decimal result;
        public IncomeView()
        {
            this.InitializeComponent();
        }

        private void FromDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            try
            {
                selectedDate = FromDate.Date.ToString();
                formatedDate = "" + selectedDate[0] + selectedDate[1] + "-" + selectedDate[3] + selectedDate[4] + "-" + selectedDate[6] + selectedDate[7] + selectedDate[8] + selectedDate[9];
                Decimal sumAmount = DAO.GetSumPaymentByDate(formatedDate);
                Amount.Text = sumAmount.ToString();
            }
            catch
            {
                Debug.WriteLine("Select a date!");
            }
            
        }

        private void DatePeriodDropDownList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DatePeriodDropDownList.SelectedIndex == 0)
            {
               result = DAO.GetTotalDay(DateTime.Now);

            }
            else if(DatePeriodDropDownList.SelectedIndex == 1)
            {
                //result = DAO.GetTotalMonth(DateTime.Now);
            }
            else if (DatePeriodDropDownList.SelectedIndex == 2)
            {
                result = DAO.GetTotalMonth(DateTime.Now);
            }
            else if (DatePeriodDropDownList.SelectedIndex == 3)
            {
                result = DAO.GetTotalMonth(DateTime.Now);
            }
            else
            {
              result = DAO.GetTotalPayments();
            }

            Amount.Text = result.ToString();
        }
    }
}
