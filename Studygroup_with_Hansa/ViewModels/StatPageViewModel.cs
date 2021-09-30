using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Models.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Studygroup_with_Hansa.ViewModels
{
    public class StatPageViewModel : ViewModelBase
    {
        private List<WeekModel> _week;
        public List<WeekModel> Week
        {
            get { return _week; }
            set { Set(ref _week, value); }
        }

        private WeekModel _selectedDay;
        public WeekModel SelectedDay
        {
            get { return _selectedDay; }
            set { Set(ref _selectedDay, value); }
        }

        public RelayCommand<object> SelectCommand { get; private set; }

        public StatPageViewModel()
        {
            Week = GetWeek(DateTime.Now);
            SelectedDay = Week[Week.Count - 1];
            SelectCommand = new RelayCommand<object>(ExecuteSelectCommand);
        }

        private List<WeekModel> GetWeek(DateTime lastDay)
        {
            List<WeekModel> thisWeek = new List<WeekModel>();
            List<SubjectModel> tempSubjects = new List<SubjectModel>();

            for (int i = 0; i < 7; i++)
            {
                tempSubjects.Add(new SubjectModel(
                    ((SubjectColor)(i % 5)).ToString().Replace("_", "#"), "과목" + i, i * i - i));
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
