using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Studygroup_with_Hansa.Views;

namespace Studygroup_with_Hansa.Controls
{
    /// <summary>
    ///     Interaction logic for PlanPageControl.xaml
    /// </summary>
    public partial class GroupPageControl : UserControl
    {
        public GroupPageControl()
        {
            InitializeComponent();
        }

        private void DelGroupButton_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            var setGoalWindow = new DelGroupWindow();

            var item = sender as Button;
            var cmd = item?.Tag as ICommand;
            cmd?.Execute(null);

            setGoalWindow.Owner = parentWindow;
            _ = setGoalWindow.ShowDialog();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("복사되었습니다");
        }
    }
}