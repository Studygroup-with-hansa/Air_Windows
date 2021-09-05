using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Studygroup_with_Hansa.Models.Types.SubjectColorType;

namespace Studygroup_with_Hansa.ViewModels
{
    class AddSubjectViewModel : ObservableObject
    {
        private string _enteredName = string.Empty;
        public string EnteredName
        {
            get { return _enteredName; }
            set
            {
                _enteredName = value;
                AddCommand.NotifyCanExecuteChanged();
            }
        }

        private SubjectColor _selectedColor = 0;
        public SubjectColor SelectedColor
        {
            get { return _selectedColor; }
            set { SetProperty(ref _selectedColor, value); }
        }

        public IRelayCommand AddCommand { get; private set; }

        public AddSubjectViewModel()
        {
            AddCommand = new RelayCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }

        private void ExecuteAddCommand()
        {
            SubjectModel toAddSubject =
                new SubjectModel(SelectedColor.ToString().Replace("_", "#"), EnteredName.Trim());
            WeakReferenceMessenger.Default.Send(new SubjectAddedMessage(toAddSubject));
        }

        private bool CanExecuteAddCommand()
        {
            return string.IsNullOrWhiteSpace(EnteredName) == false;
        }
    }
}
