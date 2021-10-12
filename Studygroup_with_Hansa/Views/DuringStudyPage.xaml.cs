using System.Windows;
using System.Windows.Controls;

namespace Studygroup_with_Hansa.Views
{
    /// <summary>
    ///     Interaction logic for DuringStudyPage.xaml
    /// </summary>
    public partial class DuringStudyPage : Page
    {
        public DuringStudyPage()
        {
            InitializeComponent();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}