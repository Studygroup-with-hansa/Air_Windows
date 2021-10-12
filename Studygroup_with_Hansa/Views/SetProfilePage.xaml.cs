using System.Windows;
using System.Windows.Controls;

namespace Studygroup_with_Hansa.Views
{
    /// <summary>
    ///     Interaction logic for SetProfilePage.xaml
    /// </summary>
    public partial class SetProfilePage : Page
    {
        public SetProfilePage()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService?.Navigate(new MainMenuPage());
        }
    }
}