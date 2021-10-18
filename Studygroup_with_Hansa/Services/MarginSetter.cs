using System.Windows;
using System.Windows.Controls;

namespace Studygroup_with_Hansa.Services
{
    public class MarginSetter
    {
        // Using a DependencyProperty as the backing store for Margin.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(MarginSetter),
                new UIPropertyMetadata(new Thickness(), Changed));

        public static Thickness GetMargin(DependencyObject obj)
        {
            return obj != null ? (Thickness) obj.GetValue(MarginProperty) : new Thickness();
        }

        public static void SetMargin(DependencyObject obj, Thickness value)
        {
            obj?.SetValue(MarginProperty, value);
        }

        public static void Changed(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Panel panel)) return;
            panel.Loaded += CreateThicknesForChildren;
        }

        public static void CreateThicknesForChildren(object sender, RoutedEventArgs e)
        {
            if (!(sender is Panel panel)) return;
            foreach (var child in panel.Children)
            {
                if (!(child is FrameworkElement fe)) continue;
                fe.Margin = GetMargin(panel);
            }
        }
    }
}