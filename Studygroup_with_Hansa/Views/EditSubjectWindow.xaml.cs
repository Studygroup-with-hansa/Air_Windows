using System.Windows;

namespace Studygroup_with_Hansa.Views
{
    /// <summary>
    ///     Interaction logic for EditSubjectWindow.xaml
    /// </summary>
    public partial class EditSubjectWindow : Window
    {
        public EditSubjectWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}