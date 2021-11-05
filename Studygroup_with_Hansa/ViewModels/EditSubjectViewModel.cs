using System.Collections.Generic;
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

        private async void EditSubject(string oldSubject)
        {
            var requestParams = new List<ParamModel>
            {
                new ParamModel("title_new", EnteredName),
                new ParamModel("title", oldSubject),
                new ParamModel("color", SelectedColor.ToString().Replace("_", "#"))
            };
            _ = await RestManager.RestRequest<string>("v1/user/data/subject/manage/", Method.PUT, requestParams);
        }

        private void ExecuteEditCommand()
        {
            var locator = (ViewModelLocator) Application.Current.Resources["Locator"];

            EditSubject(locator.HomePage.SelectedSubject.Title);
            Messenger.Default.Send(new SubjectEditedMessage(locator.HomePage.SelectedSubject,
                SelectedColor.ToString().Replace("_", "#"), EnteredName.Trim()));
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