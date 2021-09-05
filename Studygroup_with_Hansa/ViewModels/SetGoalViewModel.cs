using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Studygroup_with_Hansa.ViewModels
{
    class SetGoalViewModel : ObservableObject
    {
        private int _enteredHour;
        public int EnteredHour
        {
            get { return _enteredHour; }
            set
            {
                _enteredHour = value;
                SetCommand.NotifyCanExecuteChanged();
            }
        }

        private int _enteredMinute;
        public int EnteredMinute
        {
            get { return _enteredMinute; }
            set
            {
                _enteredMinute = value;
                SetCommand.NotifyCanExecuteChanged();
            }
        }

        private int _enteredSecond;
        public int EnteredSecond
        {
            get { return _enteredSecond; }
            set
            {
                _enteredSecond = value;
                SetCommand.NotifyCanExecuteChanged();
            }
        }

        public IRelayCommand SetCommand { get; private set; }

        public IRelayCommand OffBlurCommand { get; private set; }

        public SetGoalViewModel()
        {
            SetCommand = new RelayCommand(ExecuteSetCommand, CanExecuteSetCommand);
            OffBlurCommand = new RelayCommand(ExecuteOffBlurCommand);
        }

        private void ExecuteSetCommand()
        {
            int goal = TimeToSeconds.ToSeconds(EnteredHour, EnteredMinute, EnteredSecond);
            WeakReferenceMessenger.Default.Send(new GoalChangedMessage(goal));
            ExecuteOffBlurCommand();
        }

        private bool CanExecuteSetCommand()
        {
            return EnteredHour > 0 || EnteredMinute > 0 || EnteredSecond > 0;
        }

        private void ExecuteOffBlurCommand()
        {
            WeakReferenceMessenger.Default.Send(new IsBlurChangedMessage(false));
        }
    }
}
