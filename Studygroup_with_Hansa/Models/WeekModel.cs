using System;
using System.Collections.Generic;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;

namespace Studygroup_with_Hansa.Models
{
    public class LegendModel
    {
        public Brush Fill { get; set; }

        public string Title { get; set; }

        public string Value { get; set; }
    }

    public class WeekModel : ObservableObject
    {
        public WeekModel(DateTime day, int goal, int totalRun, List<Subject> subjects)
        {
            Day = day;
            Goal = goal;
            TotalRun = totalRun;

            Subjects = new SeriesCollection();
            Legends = new List<LegendModel>();

            subjects?.ForEach(e =>
            {
                Subjects.Add(new PieSeries
                {
                    Title = e.Title,
                    Fill = (Brush) new BrushConverter().ConvertFromString(e.Color),
                    Values = new ChartValues<int> {e.Time}
                });

                Legends.Add(new LegendModel
                {
                    Title = e.Title,
                    Fill = (Brush) new BrushConverter().ConvertFromString(e.Color),
                    Value = new TimeSpan(0, 0, 0, e.Time).ToString("hh\\:mm\\:ss")
                });
            });
        }

        public bool IsChecked { get; set; }

        public DateTime Day { get; set; }

        public int Goal { get; set; }

        public string GoalString
        {
            get
            {
                var t = TimeSpan.FromSeconds(Goal);
                return string.Format($"{t:hh}H {t:mm}M {t:ss}S");
            }
        }

        public int TotalRun { get; set; }

        public string TotalRunString
        {
            get
            {
                var t = TimeSpan.FromSeconds(TotalRun);
                return string.Format($"{t:hh}H {t:mm}M {t:ss}S");
            }
        }

        public double Achieve
        {
            get
            {
                if (TotalRun < Goal)
                    return (double) TotalRun / Goal;

                return Goal > 0 ? 1 : 0;
            }
        }

        public double Opacity => Achieve > 0 ? 0.3 + Achieve * ((double) 7 / 10) : 0;

        public bool IsStarted => TotalRun > 0;

        public SeriesCollection Subjects { get; set; }

        public List<LegendModel> Legends { get; set; }
    }
}