using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Collections;
using System.Collections.ObjectModel;

namespace Studygroup_with_Hansa.ViewModels
{
    class HomePageViewModel : ObservableObject
    {
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
                SetProperty(ref _goal, value);
                OnPropertyChanged("GoalString");
                OnPropertyChanged("Progress");
                OnPropertyChanged("ProgressLeft");
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

        public GridLength Progress
        {
            get
            {
                if (TotalRun <= 0 || Goal <= 0) return new GridLength(0);
                return new GridLength((double)TotalRun / Goal, GridUnitType.Star);
            }
        }

        public GridLength ProgressLeft
        {
            get
            {
                return new GridLength(1 - Progress.Value, GridUnitType.Star);
            }
        }

        public SubjectModel SelectedSubject { get; set; }

        public ObservableCollection<SubjectModel> Subjects { get; set; }

        public IRelayCommand SetBlurCommand { get; private set; }

        public IRelayCommand StartCommand { get; private set; }

        public IRelayCommand StopCommand { get; private set; }

        public IRelayCommand SelectCommand { get; private set; }

        public IRelayCommand DeleteCommand { get; private set; }

        private static DispatcherTimer changeDateTimer;

        public HomePageViewModel()
        {
            Subjects = new ObservableCollection<SubjectModel>();

            SetBlurCommand = new RelayCommand(ExecuteSetBlurCommand);
            StartCommand = new RelayCommand<object>(ExecuteStartCommand);
            StopCommand = new RelayCommand(ExecuteStopCommand);
            SelectCommand = new RelayCommand<object>(ExecuteSelectCommand);
            DeleteCommand = new RelayCommand<object>(ExecuteDeleteCommand);

            WeakReferenceMessenger.Default.Register<GoalChangedMessage>(this, (r, m) =>
            {
                Goal = m.Value;
            });

            WeakReferenceMessenger.Default.Register<SubjectAddedMessage>(this, (r, m) =>
            {
                Subjects.Add(m.Value);
            });

            WeakReferenceMessenger.Default.Register<SubjectEditedMessage>(this, (r, m) =>
            {
                Subjects[Subjects.IndexOf(SelectedSubject)].BtnColor = m.Value.BtnColor;
                Subjects[Subjects.IndexOf(SelectedSubject)].Name = m.Value.Name;
            });

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

        private void ExecuteSetBlurCommand()
        {
            WeakReferenceMessenger.Default.Send(new IsBlurChangedMessage(true));
        }

        private void ExecuteStartCommand(object obj)
        {
            SelectedSubject = obj as SubjectModel;
            SelectedSubject.subjectTimer.Start();
        }

        private void ExecuteStopCommand()
        {
            SelectedSubject.subjectTimer.Stop();
            RefreshPercentage();
            OnPropertyChanged("TotalRunString");
            OnPropertyChanged("Progress");
            OnPropertyChanged("ProgressLeft");
        }

        private void ExecuteSelectCommand(object obj)
        {
            SelectedSubject = obj as SubjectModel;
            ExecuteSetBlurCommand();
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
