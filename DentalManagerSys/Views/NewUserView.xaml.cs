using DentalManagerSys.ViewModel;
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
    public sealed partial class NewUserView : Page
    {
        public CreateUserViewModel ViewModel { get; set; } = new CreateUserViewModel();
        private string passwordError;
        private string nameError;
        public NewUserView()
        {
            this.InitializeComponent();
            passwordError = "Password don't match.";
            nameError = "Name is to short.";
        }

        private void NewAccountButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.test();

            if(!newPasswordBox.Password.Equals(confirmPasswordBox.Password))
            {
                ErrorMessage.Text = passwordError;
            }
            else
            {
                if(userNameInput.Text.Length<2)
                {
                    ErrorMessage.Text = nameError;
                }else
                {
                    ErrorMessage.Text = "";


                }
                
            }

            Debug.WriteLine(newPasswordBox.Password);
        }
    }
}
