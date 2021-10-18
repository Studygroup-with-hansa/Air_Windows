using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RestSharp;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Services;

namespace Studygroup_with_Hansa.ViewModels
{
    public class StatPageViewModel : ViewModelBase
    {
        private Stat _selectedDay;
        private StatModel _week;

        public StatPageViewModel()
        {
            SelectCommand = new RelayCommand<object>(ExecuteSelectCommand);

            var requestParams = new List<ParamModel>
            {
                new ParamModel("startDate", DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd")),
                new ParamModel("endDate", DateTime.Now.ToString("yyyy-MM-dd"))
            };
            var result =
                RestManager.RestRequest<ResultModel<StatModel>>("v1/user/data/stats/", Method.POST, requestParams);

            Week = result.Result.Data.Data;
            Week.Stats[Week.Stats.Count - 1].IsChecked = true;
            SelectedDay = Week.Stats[Week.Stats.Count - 1];
        }

        public StatModel Week
        {
            get => _week;
            set => Set(ref _week, value);
        }

        public Stat SelectedDay
        {
            get => _selectedDay;
            set => Set(ref _selectedDay, value);
        }

        public RelayCommand<object> SelectCommand { get; }

        private void ExecuteSelectCommand(object day)
        {
            SelectedDay = day as Stat;
        }
    }
}