using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Studygroup_with_Hansa.Services;
using Studygroup_with_Hansa.Views;

namespace Studygroup_with_Hansa.Controls
{
    /// <summary>
    ///     Interaction logic for HomePageControl.xaml
    /// </summary>
    public partial class HomePageControl : UserControl
    {
        public HomePageControl()
        {
            InitializeComponent();
        }

        private void SetGoalButton_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            var setGoalWindow = new SetGoalWindow();

            var item = sender as Button;
            var cmd = item?.Tag as ICommand;
            cmd?.Execute(item.CommandParameter);

            setGoalWindow.Owner = parentWindow;
            _ = setGoalWindow.ShowDialog();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var parentPage = PageNavigation.FindParentPage(this);
            _ = parentPage.NavigationService?.Navigate(new AddSubjectPage());
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var parentPage = PageNavigation.FindParentPage(this);
            var duringStudyPage = new DuringStudyPage
            {
                DataContext = DataContext
            };
            _ = parentPage.NavigationService?.Navigate(duringStudyPage);
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            var editSubjectWindow = new EditSubjectWindow();

            var item = sender as MenuItem;
            var cmd = item?.Tag as ICommand;
            cmd?.Execute(item.CommandParameter);

            editSubjectWindow.Owner = parentWindow;
            _ = editSubjectWindow.ShowDialog();
        }
    }
}