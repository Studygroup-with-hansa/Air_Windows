using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Studygroup_with_Hansa.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        private BottomMenu _menuNumbers = BottomMenu.Home;
        public BottomMenu MenuNumbers
        {
            get { return _menuNumbers; }
            set { Set(ref _menuNumbers, value); }
        }

        private Visibility _isBlur = Visibility.Collapsed;
        public Visibility IsBlur
        {
            get { return _isBlur; }
            set { Set(ref _isBlur, value); }
        }

        public MainMenuViewModel()
        {
            Messenger.Default.Register<IsBlurChangedMessage>(this, m =>
            {
                IsBlur = m.IsBlur ? Visibility.Visible : Visibility.Collapsed;
            });
        }
    }
}
