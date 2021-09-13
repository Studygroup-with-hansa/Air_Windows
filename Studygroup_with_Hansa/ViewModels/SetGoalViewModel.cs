using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
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
    public class SetGoalViewModel : ViewModelBase
    {
        private int _enteredHour;
        public int EnteredHour
        {
            get { return _enteredHour; }
            set
            {
                _enteredHour = value;
                SetCommand.RaiseCanExecuteChanged();
            }
        }

        private int _enteredMinute;
        public int EnteredMinute
        {
            get { return _enteredMinute; }
            set
            {
                _enteredMinute = value;
                SetCommand.RaiseCanExecuteChanged();
            }
        }

        private int _enteredSecond;
        public int EnteredSecond
        {
            get { return _enteredSecond; }
            set
            {
                _enteredSecond = value;
                SetCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand SetCommand { get; private set; }

        public RelayCommand OffBlurCommand { get; private set; }

        public SetGoalViewModel()
        {
            SetCommand = new RelayCommand(ExecuteSetCommand, CanExecuteSetCommand);
            OffBlurCommand = new RelayCommand(ExecuteOffBlurCommand);
        }

        private void ExecuteSetCommand()
        {
            int goal = TimeToSeconds.ToSeconds(EnteredHour, EnteredMinute, EnteredSecond);
            Messenger.Default.Send(new GoalChangedMessage(goal));
            ExecuteOffBlurCommand();
        }

        private bool CanExecuteSetCommand()
        {
            return EnteredHour > 0 || EnteredMinute > 0 || EnteredSecond > 0;
        }

        private void ExecuteOffBlurCommand()
        {
            Messenger.Default.Send(new IsBlurChangedMessage(false));
        }
    }
}
