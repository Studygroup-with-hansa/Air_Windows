using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
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
    public class HomePageViewModel : ViewModelBase
    {
        private static Timer changeDateTimer;

        private int _goal = -1;

        private DateTime _nowTime = DateTime.Now;

        private int _totalRun;

        private ObservableCollection<Subject> _subjects;

        public HomePageViewModel()
        {
            SetSubjects();

            SetBlurCommand = new RelayCommand(ExecuteSetBlurCommand);
            StartCommand = new RelayCommand<object>(ExecuteStartCommand);
            StopCommand = new RelayCommand(ExecuteStopCommand);
            SelectCommand = new RelayCommand<object>(ExecuteSelectCommand);
            DeleteCommand = new RelayCommand<object>(ExecuteDeleteCommand);

            Messenger.Default.Register<GoalChangedMessage>(this, m => { Goal = m.Goal; });

            Messenger.Default.Register<SubjectAddedMessage>(this, m => { Subjects.Add(m.Subject); });

            Messenger.Default.Register<SubjectEditedMessage>(this, m =>
            {
                Subjects[Subjects.IndexOf(SelectedSubject)].Color = m.Color;
                Subjects[Subjects.IndexOf(SelectedSubject)].Title = m.Title;
            });

            //SetupTimer();
        }

        public DateTime NowTime
        {
            get => _nowTime;
            set => Set(ref _nowTime, value);
        }

        public int TotalRun
        {
            get
            {
                _totalRun = 0;
                Subjects?.ToList().ForEach(e => _totalRun += e.ElapsedTime);
                return _totalRun;
            }
        }

        public string TotalRunString
        {
            get
            {
                var t = TimeSpan.FromSeconds(TotalRun);
                return string.Format($"{t:hh}H {t:mm}M {t:ss}S");
            }
        }

        public int Goal
        {
            get => _goal;
            set
            {
                _ = Set(ref _goal, value);
                RaisePropertyChanged("GoalString");
                RaisePropertyChanged("Progress");
                RaisePropertyChanged("ProgressLeft");
            }
        }

        public string GoalString
        {
            get
            {
                var t = TimeSpan.FromSeconds(Goal);
                return t.TotalSeconds > 0 ? string.Format($"{t:hh}H {t:mm}M {t:ss}S") : "00H 00M 00S";
            }
        }

        public GridLength Progress =>
            TotalRun <= 0 || Goal <= 0
                ? new GridLength(0)
                : new GridLength((double) TotalRun / Goal, GridUnitType.Star);

        public GridLength ProgressLeft =>
            Progress.Value > 1 ? new GridLength(0) : new GridLength(1 - Progress.Value, GridUnitType.Star);

        public ObservableCollection<Subject> Subjects
        {
            get => _subjects;
            set => Set(ref _subjects, value);
        }

        public Subject SelectedSubject { get; set; }

        public RelayCommand SetBlurCommand { get; }

        public RelayCommand<object> StartCommand { get; }

        public RelayCommand StopCommand { get; }

        public RelayCommand<object> SelectCommand { get; }

        public RelayCommand<object> DeleteCommand { get; }

        private async void SetSubjects()
        {
            var result = await RestManager.RestRequest<SubjectModel>("v1/user/data/subject/history/", Method.POST, null);
            Subjects = new ObservableCollection<Subject>(result.Data.SubjectList);
            Goal = result.Data.Goal == 0 ? -1 : result.Data.Goal;

            RefreshPercentage();
            RaisePropertyChanged("TotalRunString");
        }

        private async void DeleteSubject()
        {
            _ = await RestManager.RestRequest<string>("v1/user/data/subject/manage/", Method.DELETE,
                new List<ParamModel> {new ParamModel("title", SelectedSubject.Title)});
        }

        private async void StartTimer()
        {
            _ = await RestManager.RestRequest<string>("v1/user/timer/start/", Method.POST,
                new List<ParamModel> {new ParamModel("title", SelectedSubject.Title)});
        }

        private async void StopTimer()
        {
            _ = await RestManager.RestRequest<string>("v1/user/timer/stop/", Method.POST, null);
        }

        private void SetupTimer()
        {
            var nowTime = DateTime.Now;
            var midNight = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 0, 0, 0, 0).AddDays(1);
            var remainTime = (midNight - nowTime).Milliseconds;

            changeDateTimer = new Timer
            {
                Interval = remainTime
            };
            changeDateTimer.Elapsed += Change_Date;
            changeDateTimer.Start();
        }

        private void RefreshPercentage()
        {
            Subjects.ToList().ForEach(e => { e.Percentage = TotalRun <= 0 ? 0 : (double) e.ElapsedTime / TotalRun; });
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
                _ = MessageBox.Show("Exception", ex.ToString(),
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            {
                SetupTimer();
            }
        }

        private void ExecuteSetBlurCommand()
        {
            Messenger.Default.Send(new IsBlurChangedMessage(true));
        }

        private void ExecuteStartCommand(object obj)
        {
            SelectedSubject = obj as Subject;

            StartTimer();
            SelectedSubject?.SubjectTimer.Start();
        }

        private void ExecuteStopCommand()
        {
            StopTimer();
            SelectedSubject.SubjectTimer.Stop();
            RefreshPercentage();

            RaisePropertyChanged("TotalRunString");
            RaisePropertyChanged("Progress");
            RaisePropertyChanged("ProgressLeft");
        }

        private void ExecuteSelectCommand(object obj)
        {
            SelectedSubject = obj as Subject;
            ExecuteSetBlurCommand();
        }

        private void ExecuteDeleteCommand(object obj)
        {
            SelectedSubject = obj as Subject;

            DeleteSubject();
            _ = Subjects?.Remove(SelectedSubject);

            RefreshPercentage();
            RaisePropertyChanged("TotalRunString");
            RaisePropertyChanged("Progress");
        }
    }
}