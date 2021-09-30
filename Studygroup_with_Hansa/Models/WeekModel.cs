using LiveCharts;
using LiveCharts.Wpf;
using Studygroup_with_Hansa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Studygroup_with_Hansa.Models
{
    public class LegendModel
    {
        public Brush Fill { get; set; }

        public string Title { get; set; }

        public string Value { get; set; }
    }

    public class WeekModel
    {
        public bool IsChecked { get; set; }

        public DateTime Day { get; set; }

        public int Goal { get; set; }

        public string GoalString
        {
            get
            {
                int[] t = TimeToSeconds.FromSeconds(Goal);
                return string.Format($"{t[0]:00}H {t[1]:00}M {t[2]:00}S");
            }
        }

        public int TotalRun { get; set; }

        public string TotalRunString
        {
            get
            {
                int[] t = TimeToSeconds.FromSeconds(TotalRun);
                return string.Format($"{t[0]:00}H {t[1]:00}M {t[2]:00}S");
            }
        }

        public double Achieve
        {
            get => Goal > 0 ? (double)(TotalRun % Goal) / Goal * 100 : 0;
        }

        public double Opacity
        {
            get => Achieve / 100 > 0 ? 0.3 + Achieve / 100 * ((double)7 / 10) : 0;
        }

        public bool IsStarted
        {
            get => TotalRun > 0;
        }

        public SeriesCollection Subjects { get; set; }

        public List<LegendModel> Legends { get; set; }

        public WeekModel(DateTime day, int goal, int totalRun, List<SubjectModel> subjects)
        {
            IsChecked = false;
            Day = day;
            Goal = goal;
            TotalRun = totalRun;

            Subjects = new SeriesCollection();
            Legends = new List<LegendModel>();
            subjects.ForEach(e =>
            {
                Subjects.Add(new PieSeries
                {
                    Title = e.Name,
                    Fill = (Brush)new BrushConverter().ConvertFromString(e.BtnColor),
                    Values = new ChartValues<int> { e.ElapsedTime }
                });

                Legends.Add(new LegendModel
                {
                    Title = e.Name,
                    Fill = (Brush)new BrushConverter().ConvertFromString(e.BtnColor),
                    Value = e.ElapsedTimeString
                });
            });
        }
    }
}
