using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Services;

namespace Studygroup_with_Hansa.ViewModels
{
    public class SetGoalViewModel : ViewModelBase
    {
        private int _enteredHour;

        private int _enteredMinute;

        private int _enteredSecond;

        public SetGoalViewModel()
        {
            SetCommand = new RelayCommand(ExecuteSetCommand, CanExecuteSetCommand);
            OffBlurCommand = new RelayCommand(ExecuteOffBlurCommand);
        }

        public int EnteredHour
        {
            get => _enteredHour;
            set
            {
                _enteredHour = value;
                SetCommand.RaiseCanExecuteChanged();
            }
        }

        public int EnteredMinute
        {
            get => _enteredMinute;
            set
            {
                _enteredMinute = value;
                SetCommand.RaiseCanExecuteChanged();
            }
        }

        public int EnteredSecond
        {
            get => _enteredSecond;
            set
            {
                _enteredSecond = value;
                SetCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand SetCommand { get; }

        public RelayCommand OffBlurCommand { get; }

        private void ExecuteSetCommand()
        {
            var goal = EnteredHour * 60 * 60 + EnteredMinute * 60 + EnteredSecond;
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