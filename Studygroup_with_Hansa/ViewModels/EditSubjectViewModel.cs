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
    class EditSubjectViewModel : ObservableObject
    {
        private string _enteredName = string.Empty;
        public string EnteredName
        {
            get { return _enteredName; }
            set
            {
                _enteredName = value;
                EditCommand.NotifyCanExecuteChanged();
            }
        }

        private SubjectColor _selectedColor = 0;
        public SubjectColor SelectedColor
        {
            get { return _selectedColor; }
            set { SetProperty(ref _selectedColor, value); }
        }

        public IRelayCommand EditCommand { get; private set; }

        public IRelayCommand OffBlurCommand { get; private set; }

        public EditSubjectViewModel()
        {
            EditCommand = new RelayCommand(ExecuteEditCommand, CanExecuteEditCommand);
            OffBlurCommand = new RelayCommand(ExecuteOffBlurCommand);
        }

        private void ExecuteEditCommand()
        {
            SubjectModel toEditSubject =
                new SubjectModel(SelectedColor.ToString().Replace("_", "#"), EnteredName.Trim());
            WeakReferenceMessenger.Default.Send(new SubjectEditedMessage(toEditSubject));
            ExecuteOffBlurCommand();
        }

        private bool CanExecuteEditCommand()
        {
            return string.IsNullOrWhiteSpace(EnteredName) == false;
        }

        private void ExecuteOffBlurCommand()
        {
            WeakReferenceMessenger.Default.Send(new IsBlurChangedMessage(false));
        }
    }
}
