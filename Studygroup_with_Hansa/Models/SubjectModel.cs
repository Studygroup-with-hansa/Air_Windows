using System;
using System.Windows.Threading;
using GalaSoft.MvvmLight;

namespace Studygroup_with_Hansa.Models
{
    public class SubjectModel : ObservableObject
    {
        private string _btnColor;

        private int _elapsedTime;

        private string _name;

        private double _percentage;

        public DispatcherTimer SubjectTimer;

        public SubjectModel(string color, string name)
        {
            SubjectTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            SubjectTimer.Tick += Time_Elapsed;

            BtnColor = color;
            Name = name;
        }

        public string BtnColor
        {
            get => _btnColor;
            set => Set(ref _btnColor, value);
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public int ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                _ = Set(ref _elapsedTime, value);
                RaisePropertyChanged("ElapsedTimeString");
            }
        }

        public string ElapsedTimeString
        {
            get
            {
                var t = TimeSpan.FromSeconds(ElapsedTime);
                return t.ToString(@"hh\:mm\:ss");
            }
        }

        public double Percentage
        {
            get => _percentage;
            set => Set(ref _percentage, value);
        }

        private void Time_Elapsed(object sender, EventArgs e)
        {
            ElapsedTime++;
        }
    }
}