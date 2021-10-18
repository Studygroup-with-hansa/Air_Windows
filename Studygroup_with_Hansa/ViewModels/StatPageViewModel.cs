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
        private WeekModel _selectedDay;
        private List<WeekModel> _week;

        public StatPageViewModel()
        {
            Week = GetWeek(DateTime.Now.AddDays(-1));
            SelectedDay = Week[Week.Count - 1];
            SelectCommand = new RelayCommand<object>(ExecuteSelectCommand);
        }

        public List<WeekModel> Week
        {
            get => _week;
            set => Set(ref _week, value);
        }

        public WeekModel SelectedDay
        {
            get => _selectedDay;
            set => Set(ref _selectedDay, value);
        }

        public RelayCommand<object> SelectCommand { get; }

        private List<WeekModel> GetWeek(DateTime lastDay)
        {
            var thisWeek = new List<WeekModel>();

            var requestParams = new List<ParamModel>
            {
                new ParamModel("startDate", lastDay.AddDays(-6).ToString("yyyy-MM-dd")),
                new ParamModel("endDate", lastDay.ToString("yyyy-MM-dd"))
            };
            var result = RestManager.RestRequest<StatModel>("v1/user/data/stats/", Method.POST, requestParams);

            result.Result.Data.Data.Stats.ForEach(e =>
            {
                thisWeek.Add(new WeekModel(DateTime.Parse(e.Date), e.Goal, e.TotalStudyTime, e.Subject));
            });
            thisWeek[thisWeek.Count - 1].IsChecked = true;

            return thisWeek;
        }

        private void ExecuteSelectCommand(object day)
        {
            SelectedDay = day as WeekModel;
        }
    }
}