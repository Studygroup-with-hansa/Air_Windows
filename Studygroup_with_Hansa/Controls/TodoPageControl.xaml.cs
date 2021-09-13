using System;
using System.Collections.Generic;
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
    /// Interaction logic for TodoPageControl.xaml
    /// </summary>
    public partial class TodoPageControl : UserControl
    {
        public TodoPageControl()
        {
            InitializeComponent();
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            datePicker.IsDropDownOpen = true;
        }
    }
}
