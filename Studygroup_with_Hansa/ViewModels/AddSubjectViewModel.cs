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
    public class AddSubjectViewModel : ViewModelBase
    {
        private bool _validity = true;
        public bool Validity
        {
            get { return _validity; }
            set { Set(ref _validity, value); }
        }

        private string _inputName = string.Empty;
        public string InputName
        {
            get { return _inputName; }
            set
            {
                _inputName = value;
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

        readonly List<SubjectModel> subjects;

        public AddSubjectViewModel()
        {
            ViewModelLocator locator = (ViewModelLocator)Application.Current.Resources["Locator"];

            AddCommand = new RelayCommand(ExecuteAddCommand, CanExecuteAddCommand);

            subjects = locator.HomePage.Subjects.ToList();
        }

        private void ExecuteAddCommand()
        {
            SubjectModel toAddSubject =
                new SubjectModel(SelectedColor.ToString().Replace("_", "#"), InputName.Trim());
            Messenger.Default.Send(new SubjectAddedMessage(toAddSubject));
        }

        private bool CanExecuteAddCommand()
        {
            Validity = true;
            foreach(SubjectModel e in subjects)
            {
                if (e.Name == InputName.Trim())
                {
                    Validity = false;
                    return false;
                }
            }

            return string.IsNullOrWhiteSpace(InputName) == false;
        }
    }
}
