using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RestSharp;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Services;

namespace Studygroup_with_Hansa.ViewModels
{
    public class GroupPageViewModel : ViewModelBase
    {
        private bool _isSelected;

        private GroupModel _selectedGroup;

        private ObservableCollection<Group> _groups;

        public GroupPageViewModel()
        {
            SetGroups();

            SelectCommand = new RelayCommand<object>(ExecuteSelectCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
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

        public ObservableCollection<Group> Groups
        {
            get => _groups;
            set => Set(ref _groups, value);
        }

        public RelayCommand<object> SelectCommand { get; }

        public RelayCommand BackCommand { get; }

        private async void SetGroups()
        {
            var result = await RestManager.RestRequest<GroupListModel>("/v1/user/data/group/", Method.GET, null);
            Groups = new ObservableCollection<Group>(result.Data.GroupList);
        }

        private async void GetDetailGroup(Group selectedGroup)
        {
            var result = await RestManager.RestRequest<GroupModel>("/v1/user/data/group/detail/", Method.POST,
                new List<ParamModel> {new ParamModel("groupCode", selectedGroup.Code)});
            SelectedGroup = result.Data;
        }

        private void ExecuteSelectCommand(object obj)
        {
            GetDetailGroup(obj as Group);
            IsSelected = true;
        }

        private void ExecuteBackCommand()
        {
            IsSelected = false;
        }
    }
}