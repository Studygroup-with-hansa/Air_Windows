using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Models.Types;

namespace Studygroup_with_Hansa.ViewModels
{
    public class StatPageViewModel : ViewModelBase
    {
        private WeekModel _selectedDay;
        private List<WeekModel> _week;

        public StatPageViewModel()
        {
            Week = GetWeek(DateTime.Now);
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
            var tempSubjects = new List<SubjectModel>();

            for (var i = 0; i < 7; i++)
            {
                tempSubjects.Add(new SubjectModel(
                    ((SubjectColor) (i % 5)).ToString().Replace("_", "#"), "과목" + i, i * i - i));
                thisWeek.Insert(0, new WeekModel(lastDay, i * 10, i * 5, tempSubjects));
                lastDay = lastDay.AddDays(-1);
            }

            thisWeek[thisWeek.Count - 1].IsChecked = true;

            return thisWeek;
        }

        private void ExecuteSelectCommand(object day)
        {
            SelectedDay = day as WeekModel;
        }
    }
}