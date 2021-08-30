using Microsoft.Toolkit.Mvvm.ComponentModel;
using Studygroup_with_Hansa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studygroup_with_Hansa.Models
{
    class SubjectModel : ObservableObject
    {
        public int Goal { get; set; }

        public string BtnColor { get; set; }

        public string Name { get; set; }

        private int _elapsedTime = 0;
        public int ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                SetProperty(ref _elapsedTime, value);
                OnPropertyChanged("ElapsedTimeString");
                OnPropertyChanged("Percentage");
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

        public string Percentage
        {
            get
            {
                if (Goal < ElapsedTime) return "100%";
                else return string.Format("{0:0.#}%", (double)ElapsedTime / Goal * 100);
            }
        }

        public SubjectModel(int goal, string color, string name)
        {
            Goal = goal;
            BtnColor = color;
            Name = name;
        }
    }
}
