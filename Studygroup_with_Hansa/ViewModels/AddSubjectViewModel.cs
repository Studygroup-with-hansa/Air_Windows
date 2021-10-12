using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Models.Types;

namespace Studygroup_with_Hansa.ViewModels
{
    public class AddSubjectViewModel : ViewModelBase
    {
        private readonly List<SubjectModel> subjects;

        private string _inputName = string.Empty;

        private SubjectColor _selectedColor = 0;
        private bool _validity = true;

        public AddSubjectViewModel()
        {
            var locator = (ViewModelLocator) Application.Current.Resources["Locator"];

            AddCommand = new RelayCommand(ExecuteAddCommand, CanExecuteAddCommand);

            subjects = locator.HomePage.Subjects.ToList();
        }

        public bool Validity
        {
            get => _validity;
            set => Set(ref _validity, value);
        }

        public string InputName
        {
            get => _inputName;
            set
            {
                _inputName = value;
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public SubjectColor SelectedColor
        {
            get => _selectedColor;
            set => Set(ref _selectedColor, value);
        }

        public RelayCommand AddCommand { get; }

        private void ExecuteAddCommand()
        {
            var toAddSubject =
                new SubjectModel(SelectedColor.ToString().Replace("_", "#"), InputName.Trim());
            Messenger.Default.Send(new SubjectAddedMessage(toAddSubject));
        }

        private bool CanExecuteAddCommand()
        {
            return subjects.All(e => e.Name != InputName.Trim())
                ? Validity = string.IsNullOrWhiteSpace(InputName) == false
                : Validity = false;
        }
    }
}