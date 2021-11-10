using System.Windows;
using System.Windows.Controls;
using Studygroup_with_Hansa.ViewModels;

namespace Studygroup_with_Hansa.Views
{
    /// <summary>
    ///     Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.ResizeMode = ResizeMode.CanMinimize;

            if (DataContext is LoginViewModel vm)
                vm.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(vm.LoginState) && vm.LoginState == true)
                    {
                        _ = vm.IsAccExist
                            ? NavigationService?.Navigate(new MainMenuPage())
                            : NavigationService?.Navigate(new SetProfilePage());
                    }
                };
        }
    }
}