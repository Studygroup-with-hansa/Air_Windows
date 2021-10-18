using System.Windows.Controls;
using System.Windows.Input;

namespace Studygroup_with_Hansa.Controls
{
    /// <summary>
    ///     Interaction logic for TodoPageControl.xaml
    /// </summary>
    public partial class TodoPageControl : UserControl
    {
        public TodoPageControl()
        {
            InitializeComponent();
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainDatePicker.IsDropDownOpen = true;
        }
    }
}