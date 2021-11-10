using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using Studygroup_with_Hansa.ViewModels;

namespace Studygroup_with_Hansa
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static readonly string ServerUrl = "http://13.124.172.240:8080/";

        public App()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            var locator = (ViewModelLocator)Current.Resources["Locator"];
            locator.TodoPage.SaveMemo(locator.TodoPage.SelectedDate.ToString("yyyy-MM-dd"));
        }
    }
}