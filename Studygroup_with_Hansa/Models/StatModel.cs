using System;
using System.Collections.Generic;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class StatSubject
    {
        [DeserializeAs(Name = "title")] public string Title { get; set; }

        [DeserializeAs(Name = "time")] public int Time { get; set; }

        [DeserializeAs(Name = "color")] public string Color { get; set; }

        public Brush Fill => (Brush)new BrushConverter().ConvertFromString(Color);
    }

    public class Stat
    {
        public bool IsChecked { get; set; }

        public DateTime Date => DateTime.Parse(DateString);

        [DeserializeAs(Name = "date")] public string DateString { get; set; }

        [DeserializeAs(Name = "totalStudyTime")] public int TotalStudyTime { get; set; }

        public string TotalStudyTimeString
        {
            get
            {
                var t = TimeSpan.FromSeconds(TotalStudyTime);
                return string.Format($"{t:hh}H {t:mm}M {t:ss}S");
            }
        }

        [DeserializeAs(Name = "subject")] public List<StatSubject> Subjects { get; set; }

        [DeserializeAs(Name = "goal")] public int Goal { get; set; }

        public string GoalString
        {
            get
            {
                var t = TimeSpan.FromSeconds(Goal);
                return string.Format($"{t:hh}H {t:mm}M {t:ss}S");
            }
        }

        public double Achieve => TotalStudyTime < Goal ? (double)TotalStudyTime / Goal : Goal > 0 ? 1 : 0;

        public double Opacity => Achieve > 0 ? 0.3 + Achieve * ((double)7 / 10) : 0;

        public bool IsStarted => TotalStudyTime > 0;

        public SeriesCollection SubjectSeries
        {
            get
            {
                var series = new SeriesCollection();

                Subjects?.ForEach(e =>
                {
                    series.Add(new PieSeries
                    {
                        Title = e.Title,
                        Fill = e.Fill,
                        Values = new ChartValues<int> { e.Time }
                    });
                });

                return series;
            }
        }
    }

    public class StatModel
    {
        [DeserializeAs(Name = "totalTime")] public int TotalTime { get; set; }

        [DeserializeAs(Name = "goals")] public int Goals { get; set; }

        [DeserializeAs(Name = "stats")] public List<Stat> Stats { get; set; }
    }
}