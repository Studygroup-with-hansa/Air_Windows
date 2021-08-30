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
        private string[] specifiedColor = { "#ED6A5E", "#F6C343", "#79D16E", "#97BAFF", "#8886FF" };

        private DateTime _nowTime = DateTime.Now;
        public DateTime NowTime
        {
            get { return _nowTime; }
            set { SetProperty(ref _nowTime, value); }
        }

        public int TotalRun { get; set; }

        public string TotalRunString
        {
            get
            {
                TotalRun = 0;

                foreach (SubjectModel e in Subjects)
                {
                    TotalRun += e.ElapsedTime;
                }

                int[] t = TimeToSeconds.FromSeconds(TotalRun);
                return string.Format($"{t[0]:00}H {t[1]:00}M {t[2]:00}S");
            }
        }

        public ObservableCollection<SubjectModel> Subjects { get; set; }

        public IRelayCommand StartCommand { get; private set; }

        private static DispatcherTimer timer;

        public HomePageViewModel()
        {
            Subjects = new ObservableCollection<SubjectModel>();
            Subjects.Add(new SubjectModel(TimeToSeconds.ToSeconds(3, 0, 0),
                specifiedColor[Subjects.Count % specifiedColor.Length], "국어"));
            Subjects.Add(new SubjectModel(TimeToSeconds.ToSeconds(3, 0, 0),
                specifiedColor[Subjects.Count % specifiedColor.Length], "수학"));

            StartCommand = new RelayCommand<object>(ExecuteStartCommand);

            SetupTimer();
        }

        private void SetupTimer()
        {
            DateTime nowTime = DateTime.Now;
            DateTime midNight = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 0, 0, 0, 0).AddDays(1);
            long remainTime = (midNight - nowTime).Ticks;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(remainTime);
            timer.Tick += new EventHandler(timer_Elapsed);
            timer.Start();
        }

        private void timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                timer.Stop();
                NowTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                timer.Stop();
                MessageBox.Show("Exception", ex.ToString(),
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            {
                SetupTimer();
            }
        }

        private void ExecuteStartCommand(object obj)
        {
            SubjectModel SelectedSubject = obj as SubjectModel;

            // Code here
            OnPropertyChanged("TotalRunString");
        }
    }
}
