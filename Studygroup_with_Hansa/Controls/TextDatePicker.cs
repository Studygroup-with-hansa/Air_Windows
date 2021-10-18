using System.Windows;
using System.Windows.Controls;

namespace Studygroup_with_Hansa.Controls
{
    public class TextDatePicker : DatePicker
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template.FindName("PART_Button", this) is Button button) button.Visibility = Visibility.Collapsed;
        }
    }
}