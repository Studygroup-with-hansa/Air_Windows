using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studygroup_with_Hansa.ViewModels
{
    public class AddSubjectViewModel : ViewModelBase
    {
        private string _enteredName = string.Empty;
        public string EnteredName
        {
            get { return _enteredName; }
            set
            {
                _enteredName = value;
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private SubjectColor _selectedColor = 0;
        public SubjectColor SelectedColor
        {
            get { return _selectedColor; }
            set { Set(ref _selectedColor, value); }
        }

        public RelayCommand AddCommand { get; private set; }

        public AddSubjectViewModel()
        {
            AddCommand = new RelayCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }

        private void ExecuteAddCommand()
        {
            SubjectModel toAddSubject =
                new SubjectModel(SelectedColor.ToString().Replace("_", "#"), EnteredName.Trim());
            Messenger.Default.Send(new SubjectAddedMessage(toAddSubject));
        }

        private bool CanExecuteAddCommand()
        {
            return string.IsNullOrWhiteSpace(EnteredName) == false;
        }
    }
}
