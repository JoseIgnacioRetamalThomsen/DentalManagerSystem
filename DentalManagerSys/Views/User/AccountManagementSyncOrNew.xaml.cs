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
    /// Options for acount management for first time use in a pc.
    /// </summary>
    public sealed partial class AccountManagementSyncOrNew : Page
    {
        /// <summary>
        /// create object
        /// </summary>
        public AccountManagementSyncOrNew()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Navigate to new account view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewAccountButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewUserView));
        }

        /// <summary>
        /// Navigate to syncrhonize account view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SyncAccountButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SyncAccountView));
        }

        /// <summary>
        /// Navigate to previus page: login page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
