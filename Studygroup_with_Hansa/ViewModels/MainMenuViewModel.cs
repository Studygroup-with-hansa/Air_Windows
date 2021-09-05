using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Studygroup_with_Hansa.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Studygroup_with_Hansa.Models.Types.MenuType;

namespace Studygroup_with_Hansa.ViewModels
{
    class MainMenuViewModel : ObservableObject
    {
        private MenuNumber _menuNumbers = MenuNumber.Home;
        public MenuNumber MenuNumbers
        {
            get { return _menuNumbers; }
            set { SetProperty(ref _menuNumbers, value); }
        }

        private Visibility _isBlur = Visibility.Collapsed;
        public Visibility IsBlur
        {
            get { return _isBlur; }
            set { SetProperty(ref _isBlur, value); }
        }

        public MainMenuViewModel()
        {
            WeakReferenceMessenger.Default.Register<IsBlurChangedMessage>(this, (r, m) =>
            {
                IsBlur = m.Value ? Visibility.Visible : Visibility.Collapsed;
            });
        }
    }
}
