using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Models.Types;

namespace Studygroup_with_Hansa.ViewModels
{
    public class EditSubjectViewModel : ViewModelBase
    {
        private string _enteredName = string.Empty;

        private SubjectColor _selectedColor = 0;

        public EditSubjectViewModel()
        {
            EditCommand = new RelayCommand(ExecuteEditCommand, CanExecuteEditCommand);
            OffBlurCommand = new RelayCommand(ExecuteOffBlurCommand);
        }

        public string EnteredName
        {
            get => _enteredName;
            set
            {
                _enteredName = value;
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        public SubjectColor SelectedColor
        {
            get => _selectedColor;
            set => Set(ref _selectedColor, value);
        }

        public RelayCommand EditCommand { get; }

        public RelayCommand OffBlurCommand { get; }

        private void ExecuteEditCommand()
        {
            var toEditSubject =
                new SubjectModel(SelectedColor.ToString().Replace("_", "#"), EnteredName.Trim());
            var locator = (ViewModelLocator) Application.Current.Resources["Locator"];

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