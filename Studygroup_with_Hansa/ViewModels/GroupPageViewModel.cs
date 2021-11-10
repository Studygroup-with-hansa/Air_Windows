using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RestSharp;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Services;

namespace Studygroup_with_Hansa.ViewModels
{
    public class GroupPageViewModel : ViewModelBase
    {
        private bool _isSelected;

        private GroupModel _selectedGroup;

        private List<Group> _groups;

        public GroupPageViewModel()
        {
            SetGroups();

            SetBlurCommand = new RelayCommand(ExecuteSetBlurCommand);
            OffBlurCommand = new RelayCommand(ExecuteOffBlurCommand);
            AddCommand = new RelayCommand(ExecuteAddCommand);
            DelCommand = new RelayCommand(ExecuteDelCommand);
            SelectCommand = new RelayCommand<object>(ExecuteSelectCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
            CopyCommand = new RelayCommand(ExecuteCopyCommand);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }

        public GroupModel SelectedGroup
        {
            get => _selectedGroup;
            set => Set(ref _selectedGroup, value);
        }

        public List<Group> Groups
        {
            get => _groups;
            set => Set(ref _groups, value);
        }

        public RelayCommand SetBlurCommand { get; }

        public RelayCommand OffBlurCommand { get; }

        public RelayCommand AddCommand { get; }

        public RelayCommand DelCommand { get; }

        public RelayCommand<object> SelectCommand { get; }

        public RelayCommand BackCommand { get; }

        public RelayCommand CopyCommand { get; }

        private async void SetGroups()
        {
            var result = await RestManager.RestRequest<GroupListModel>("/v1/user/data/group/", Method.GET, null);
            Groups = result.Data.GroupList;
        }

        private async void GetDetailGroup(Group selectedGroup)
        {
            var result = await RestManager.RestRequest<GroupModel>("/v1/user/data/group/detail/", Method.POST,
                new List<ParamModel> {new ParamModel("groupCode", selectedGroup.Code)});
            SelectedGroup = result.Data;
        }

        private void ExecuteSetBlurCommand()
        {
            Messenger.Default.Send(new IsBlurChangedMessage(true));
        }

        private void ExecuteOffBlurCommand()
        {
            Messenger.Default.Send(new IsBlurChangedMessage(false));
        }

        private async void ExecuteAddCommand()
        {
            _ = await RestManager.RestRequest<string>("/v1/user/data/group/", Method.POST, null);
            SetGroups();
        }

        private async void ExecuteDelCommand()
        {
            _ = await RestManager.RestRequest<string>("/v1/user/data/group/", Method.DELETE, null);
            ExecuteOffBlurCommand();
            ExecuteBackCommand();
        }

        private void ExecuteSelectCommand(object obj)
        {
            GetDetailGroup(obj as Group);
            IsSelected = true;
        }

        private void ExecuteBackCommand()
        {
            SetGroups();
            IsSelected = false;
        }

        private void ExecuteCopyCommand()
        {
            Clipboard.SetText(SelectedGroup.Code);
        }
    }
}