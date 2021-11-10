using System.Windows;
using System.Windows.Controls;
using Studygroup_with_Hansa.Properties;
using Studygroup_with_Hansa.Services;
using Studygroup_with_Hansa.Views;

namespace Studygroup_with_Hansa.Controls
{
    /// <summary>
    ///     Interaction logic for MemoPageControl.xaml
    /// </summary>
    public partial class SettingPageControl : UserControl
    {
        public SettingPageControl()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var parentPage = PageNavigation.FindParentPage(this);
            _ = parentPage.NavigationService?.Navigate(new LoginPage());
            Settings.Default.Token = string.Empty;
            Settings.Default.Save();
        }
    }
}