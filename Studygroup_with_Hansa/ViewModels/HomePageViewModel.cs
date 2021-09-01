using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Services;
using System.Collections;
using System.Windows.Threading;

namespace Studygroup_with_Hansa.ViewModels
{
    class HomePageViewModel : ObservableObject
    {
        public enum SubjectColor { _ED6A5E, _F6C343, _79D16E, _97BAFF, _8886FF }

        private DateTime _nowTime = DateTime.Now;
        public DateTime NowTime
        {
            get { return _nowTime; }
            set { SetProperty(ref _nowTime, value); }
        }

        private int _goal = -1;
        public int Goal
        {
            get { return _goal; }
            set
            {
                _goal = value;
                OnPropertyChanged("GoalString");
                OnPropertyChanged("Progress");
            }
        }

        public string GoalString
        {
            get
            {
                if (Goal <= 0) return "00H 00M 00S";

                int[] t = TimeToSeconds.FromSeconds(Goal);
                return string.Format($"{t[0]:00}H {t[1]:00}M {t[2]:00}S");
            }
        }

        private int _totalRun;
        public int TotalRun
        {
            get
            {
                _totalRun = 0;
                Subjects.ToList().ForEach(e => _totalRun += e.ElapsedTime);
                return _totalRun;
            }
        }

        public string TotalRunString
        {
            get
            {
                int[] t = TimeToSeconds.FromSeconds(TotalRun);
                return string.Format($"{t[0]:00}H {t[1]:00}M {t[2]:00}S");
            }
        }

        public double Progress
        {
            get
            {
                if (TotalRun <= 0) return 0;
                return (double)TotalRun / Goal * 100;
            }
        }

        private string _enteredName;
        public string EnteredName
        {
            get { return _enteredName; }
            set
            {
                _enteredName = value;
                AddCommand.NotifyCanExecuteChanged();
            }
        }

        private SubjectColor _selectedColor;
        public SubjectColor SelectedColor
        {
            get { return _selectedColor; }
            set { SetProperty(ref _selectedColor, value); }
        }

        public SubjectModel SelectedSubject { get; set; }

        public ObservableCollection<SubjectModel> Subjects { get; set; }

        public IRelayCommand StartCommand { get; private set; }

        public IRelayCommand StopCommand { get; private set; }

        public IRelayCommand AddCommand { get; private set; }

        public IRelayCommand DeleteCommand { get; private set; }

        private static DispatcherTimer changeDateTimer;
        private static DispatcherTimer subjectTimer;

        public HomePageViewModel()
        {
            Subjects = new ObservableCollection<SubjectModel>();

            StartCommand = new RelayCommand<object>(ExecuteStartCommand);
            StopCommand = new RelayCommand(ExecuteStopCommand);
            AddCommand = new RelayCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new RelayCommand<object>(ExecuteDeleteCommand);

            SetupTimer();
        }

        private void SetupTimer()
        {
            DateTime nowTime = DateTime.Now;
            DateTime midNight = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 0, 0, 0, 0).AddDays(1);
            long remainTime = (midNight - nowTime).Ticks;

            changeDateTimer = new DispatcherTimer();
            changeDateTimer.Interval = new TimeSpan(remainTime);
            changeDateTimer.Tick += new EventHandler(Change_Date);
            changeDateTimer.Start();
        }

        private void RefreshPercentage()
        {
            Subjects.ToList().ForEach(e =>
            {
                if (TotalRun <= 0) e.Percentage = 0;
                else e.Percentage = (double)e.ElapsedTime / TotalRun * 100;
            });
        }

        private void Change_Date(object sender, EventArgs e)
        {
            try
            {
                changeDateTimer.Stop();
                NowTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                changeDateTimer.Stop();
                MessageBox.Show("Exception", ex.ToString(),
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            {
                SetupTimer();
            }
        }

        private void Time_Elapsed(object sender, EventArgs e)
        {
            SelectedSubject.ElapsedTime++;
        }

        private void ExecuteStartCommand(object obj)
        {
            SelectedSubject = obj as SubjectModel;

            subjectTimer = new DispatcherTimer();
            subjectTimer.Interval = new TimeSpan(0, 0, 1);
            subjectTimer.Tick += new EventHandler(Time_Elapsed);
            subjectTimer.Start();
        }

        private void ExecuteStopCommand()
        {
            subjectTimer.Stop();
            RefreshPercentage();
            OnPropertyChanged("TotalRunString");
            OnPropertyChanged("Progress");
        }

        private void ExecuteAddCommand()
        {
            Subjects.Add(new SubjectModel(SelectedColor.ToString().Replace("_", "#"), EnteredName.Trim()));
            EnteredName = string.Empty;
        }

        private bool CanExecuteAddCommand()
        {
            return string.IsNullOrWhiteSpace(EnteredName) == false;
        }

        private void ExecuteDeleteCommand(object obj)
        {
            SelectedSubject = obj as SubjectModel;
            Subjects.Remove(SelectedSubject);

            RefreshPercentage();
            OnPropertyChanged("TotalRunString");
            OnPropertyChanged("Progress");
        }
    }
}
