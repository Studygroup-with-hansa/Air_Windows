using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models.Types;

namespace Studygroup_with_Hansa.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        private Visibility _isBlur = Visibility.Collapsed;
        private BottomMenu _menuNumbers = BottomMenu.Home;

        public MainMenuViewModel()
        {
            Messenger.Default.Register<IsBlurChangedMessage>(this,
                m => { IsBlur = m.IsBlur ? Visibility.Visible : Visibility.Collapsed; });
        }

        public BottomMenu MenuNumbers
        {
            get => _menuNumbers;
            set => Set(ref _menuNumbers, value);
        }

        public Visibility IsBlur
        {
            get => _isBlur;
            set => Set(ref _isBlur, value);
        }
    }
}