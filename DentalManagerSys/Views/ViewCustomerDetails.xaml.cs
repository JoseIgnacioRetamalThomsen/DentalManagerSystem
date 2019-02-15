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

namespace DentalManagerSys.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
   
    public sealed partial class ViewCustomerDetails : Page
    {
        private string iD;
        public ViewCustomerDetails()
        {
            this.InitializeComponent();
            DisplayDetails();
        }

        private void DisplayDetails()
        {
            Customer temp = null;//get customer from database using iD

            IdTextBox.Text = "g00234232";//temp.iD

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
               
            }
            else
            {
               Debug.WriteLine(e.Parameter);
                iD = e.Parameter.ToString();
            }

           
            
        }
    }
}
