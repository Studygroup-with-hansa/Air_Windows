using System.ComponentModel;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace Studygroup_with_Hansa.Controls
{
    /// <summary>
    ///     Interaction logic for SubjectsTooltip.xaml
    /// </summary>
    public partial class SubjectsTooltip : UserControl, IChartTooltip
    {
        private TooltipData _data;

        public SubjectsTooltip()
        {
            InitializeComponent();
            DataContext = this;
        }

        public TooltipData Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        public TooltipSelectionMode? SelectionMode { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}