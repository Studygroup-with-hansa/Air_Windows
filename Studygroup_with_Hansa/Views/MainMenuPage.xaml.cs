using System.Windows;
using System.Windows.Controls;

namespace Studygroup_with_Hansa.Views
{
    /// <summary>
    ///     Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
        }
    }
}