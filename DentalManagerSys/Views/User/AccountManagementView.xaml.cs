///------------------------------------------
///
///  Dental Manager System
///  Profesional Practice in IT project
///  GMIT 2019
///  
///  Markm Ndpeanoch
///  Jose I. Retamal
///------------------------------------------
///
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DentalManagerSys.Views
{
    /// <summary>
    /// Main view for account management
    /// </summary>
    public sealed partial class AccountManagementView : Page
    {
        /// <summary>
        /// create object
        /// </summary>
        public AccountManagementView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// navigate to reset password view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetPassword_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ResetPasswordView));
        }

        /// <summary>
        /// Back button, navigate to previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();

        }
    }
}
