using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Studygroup_with_Hansa.Controls
{
    class TextDatePicker:DatePicker
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template.FindName("PART_Button", this) is Button button)
            {
                button.Visibility = Visibility.Collapsed;
            }
        }
    }
}
