using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RestSharp;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Models.Types;
using Studygroup_with_Hansa.Services;

namespace Studygroup_with_Hansa.ViewModels
{
    public class AddSubjectViewModel : ViewModelBase
    {
        private readonly List<Subject> subjects;

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

        private async void AddSubject()
        {
            var requestParams = new List<ParamModel>
            {
                new ParamModel("title", InputName),
                new ParamModel("color", SelectedColor.ToString().Replace("_", "#"))
            };
            _ = await RestManager.RestRequest<string>("v1/user/data/subject/manage/", Method.POST, requestParams);
        }

        private void ExecuteAddCommand()
        {
            AddSubject();

            var toAddSubject = new Subject
            {
                Color = SelectedColor.ToString().Replace("_", "#"),
                Title = InputName.Trim()
            };
            Messenger.Default.Send(new SubjectAddedMessage(toAddSubject));
        }

        private bool CanExecuteAddCommand()
        {
            Validity = true;
            return subjects.All(e => e.Title != InputName.Trim())
                ? string.IsNullOrWhiteSpace(InputName) == false
                : Validity = false;
        }
    }
}