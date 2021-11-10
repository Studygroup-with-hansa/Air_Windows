using System;
using System.Collections.Generic;
using System.Timers;
using GalaSoft.MvvmLight;
using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class Subject : ObservableObject
    {
        private string _color;

        private int _elapsedTime;

        private double _percentage;

        private string _title;

        public Timer SubjectTimer;

        public Subject()
        {
            SubjectTimer = new Timer
            {
                Interval = 1000
            };
            SubjectTimer.Elapsed += (sender, e) => ElapsedTime++;
        }

        [DeserializeAs(Name = "color")]
        public string Color
        {
            get => _color;
            set => Set(ref _color, value);
        }

        [DeserializeAs(Name = "time")]
        public int ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                _elapsedTime = value;
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

        [DeserializeAs(Name = "title")]
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }

    public class SubjectModel : ObservableObject
    {
        [DeserializeAs(Name = "subject")] public List<Subject> SubjectList { get; set; } = new List<Subject>();

        [DeserializeAs(Name = "goal")] public int Goal { get; set; }
    }
}