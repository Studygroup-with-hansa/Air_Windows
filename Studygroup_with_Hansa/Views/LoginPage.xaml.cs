using System.Windows;
using System.Windows.Controls;

namespace Studygroup_with_Hansa.Views
{
    /// <summary>
    ///     Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.ResizeMode = ResizeMode.CanMinimize;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService?.Navigate(new SetProfilePage());
        }
    }
}