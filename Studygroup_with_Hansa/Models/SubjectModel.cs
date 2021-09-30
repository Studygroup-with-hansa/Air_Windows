using GalaSoft.MvvmLight;
using Studygroup_with_Hansa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Studygroup_with_Hansa.Models
{
    public class SubjectModel : ObservableObject
    {
        public DispatcherTimer subjectTimer;

        private string _btnColor;
        public string BtnColor
        {
            get { return _btnColor; }
            set { Set(ref _btnColor, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private int _elapsedTime = 0;
        public int ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                Set(ref _elapsedTime, value);
                RaisePropertyChanged("ElapsedTimeString");
            }
        }

        public string ElapsedTimeString
        {
            get
            {
                int[] t = TimeToSeconds.FromSeconds(ElapsedTime);
                return string.Format($"{t[0]:00}:{t[1]:00}:{t[2]:00}");
            }
        }

        private double _percentage = 0;
        public double Percentage
        {
            get { return _percentage; }
            set { Set(ref _percentage, value); }
        }

        public SubjectModel(string color, string name)
        {
            subjectTimer = new DispatcherTimer();
            subjectTimer.Interval = new TimeSpan(0, 0, 1);
            subjectTimer.Tick += new EventHandler(Time_Elapsed);

            BtnColor = color;
            Name = name;
        }

        public SubjectModel(string color, string name, int elapsedTime)
        {
            BtnColor = color;
            Name = name;
            ElapsedTime = elapsedTime;
        }

        private void Time_Elapsed(object sender, EventArgs e)
        {
            ElapsedTime++;
        }
    }
}
