using System.Windows;

namespace Studygroup_with_Hansa.Views
{
    /// <summary>
    ///     Interaction logic for SetGoalWindow.xaml
    /// </summary>
    public partial class SetGoalWindow : Window
    {
        public SetGoalWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}