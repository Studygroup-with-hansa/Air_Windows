using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Studygroup_with_Hansa.Controls
{
    /// <summary>
    /// Interaction logic for SubjectsLegend.xaml
    /// </summary>
    public partial class SubjectsLegend : UserControl
    {
        public SubjectsLegend()
        {
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer sv = sender as ScrollViewer;
            if (0 > e.Delta) sv.LineRight();
            else sv.LineLeft();
            e.Handled = true;
        }
    }
}
