using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Studygroup_with_Hansa.ViewModels
{
    class MainMenuPageViewModel : ObservableObject
    {
        public enum MenuNumber { Stat, Plan, Home, Todo, Memo }

        private MenuNumber _menuNumbers = MenuNumber.Home;
        public MenuNumber MenuNumbers
        {
            get { return _menuNumbers; }
            set { SetProperty(ref _menuNumbers, value); }
        }
    }
}
