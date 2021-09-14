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
using System.Windows;

namespace Studygroup_with_Hansa.ViewModels
{
    public class EditSubjectViewModel : ViewModelBase
    {
        private string _enteredName = string.Empty;
        public string EnteredName
        {
            get { return _enteredName; }
            set
            {
                _enteredName = value;
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        private SubjectColor _selectedColor = 0;
        public SubjectColor SelectedColor
        {
            get { return _selectedColor; }
            set { Set(ref _selectedColor, value); }
        }

        public RelayCommand EditCommand { get; private set; }

        public RelayCommand OffBlurCommand { get; private set; }

        public EditSubjectViewModel()
        {
            EditCommand = new RelayCommand(ExecuteEditCommand, CanExecuteEditCommand);
            OffBlurCommand = new RelayCommand(ExecuteOffBlurCommand);
        }

        private void ExecuteEditCommand()
        {
            SubjectModel toEditSubject =
                new SubjectModel(SelectedColor.ToString().Replace("_", "#"), EnteredName.Trim());
            ViewModelLocator locator = (ViewModelLocator)Application.Current.Resources["Locator"];

            Messenger.Default.Send(new SubjectEditedMessage(locator.HomePage.SelectedSubject.Name, toEditSubject));
            ExecuteOffBlurCommand();
        }

        private bool CanExecuteEditCommand()
        {
            return string.IsNullOrWhiteSpace(EnteredName) == false;
        }

        private void ExecuteOffBlurCommand()
        {
            Messenger.Default.Send(new IsBlurChangedMessage(false));
        }
    }
}
