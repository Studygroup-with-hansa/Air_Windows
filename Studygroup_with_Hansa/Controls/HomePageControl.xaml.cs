using Studygroup_with_Hansa.Services;
using Studygroup_with_Hansa.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Studygroup_with_Hansa.Controls
{
    /// <summary>
    /// Interaction logic for HomePageControl.xaml
    /// </summary>
    public partial class HomePageControl : UserControl
    {
        public HomePageControl()
        {
            InitializeComponent();
        }

        private void SetGoalButton_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            SetGoalWindow setGoalWindow = new SetGoalWindow();

            Button btn = sender as Button;
            ICommand cmd = btn.Tag as ICommand;
            cmd.Execute(null);

            setGoalWindow.Owner = parentWindow;
            setGoalWindow.ShowDialog();
        }
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Page parentPage = PageNavigation.FindParentPage(this);
            AddSubjectPage addSubjectPage = new AddSubjectPage();
            parentPage.NavigationService.Navigate(addSubjectPage);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Page parentPage = PageNavigation.FindParentPage(this);
            DuringStudyPage duringStudyPage = new DuringStudyPage();
            duringStudyPage.DataContext = this.DataContext;
            parentPage.NavigationService.Navigate(duringStudyPage);
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            EditSubjectWindow editSubjectWindow = new EditSubjectWindow();

            MenuItem item = sender as MenuItem;
            ICommand cmd = item.Tag as ICommand;
            cmd.Execute(item.CommandParameter);

            editSubjectWindow.Owner = parentWindow;
            editSubjectWindow.ShowDialog();
        }
    }
}
