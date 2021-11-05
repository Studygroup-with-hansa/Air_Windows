/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Studygroup_with_Hansa"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Studygroup_with_Hansa.ViewModels
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainMenuViewModel>();
            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<TodoPageViewModel>();
            SimpleIoc.Default.Register<StatPageViewModel>();
            SimpleIoc.Default.Register<GroupPageViewModel>();
        }

        public MainMenuViewModel MainMenu => ServiceLocator.Current.GetInstance<MainMenuViewModel>();

        public HomePageViewModel HomePage => ServiceLocator.Current.GetInstance<HomePageViewModel>();

        public TodoPageViewModel TodoPage => ServiceLocator.Current.GetInstance<TodoPageViewModel>();

        public StatPageViewModel StatPage => ServiceLocator.Current.GetInstance<StatPageViewModel>();

        public GroupPageViewModel GroupPage => ServiceLocator.Current.GetInstance<GroupPageViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}