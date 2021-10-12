using System.Windows;
using System.Windows.Controls;

namespace Studygroup_with_Hansa.Views
{
    /// <summary>
    ///     Interaction logic for AddSubjectPage.xaml
    /// </summary>
    public partial class AddSubjectPage : Page
    {
        public AddSubjectPage()
        {
            InitializeComponent();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}