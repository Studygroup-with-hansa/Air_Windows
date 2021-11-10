using System.Windows;

namespace Studygroup_with_Hansa.Views
{
    /// <summary>
    ///     Interaction logic for DelGroupWindow.xaml
    /// </summary>
    public partial class DelGroupWindow : Window
    {
        public DelGroupWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}