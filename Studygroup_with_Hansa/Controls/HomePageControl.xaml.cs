using Studygroup_with_Hansa.Services;
using Studygroup_with_Hansa.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Page parentPage = PageNavigation.FindParentPage(this);
            DuringStudyPage duringStudyPage = new DuringStudyPage(parentPage);
            duringStudyPage.DataContext = this.DataContext;
            parentPage.NavigationService.Navigate(duringStudyPage);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Page parentPage = PageNavigation.FindParentPage(this);
            AddSubjectPage addSubjectPage = new AddSubjectPage(parentPage);
            addSubjectPage.DataContext = this.DataContext;
            parentPage.NavigationService.Navigate(addSubjectPage);
        }
    }
}
