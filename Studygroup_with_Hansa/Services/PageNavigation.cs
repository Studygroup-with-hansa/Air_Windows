using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Studygroup_with_Hansa.Services
{
    internal class PageNavigation
    {
        public static Page FindParentPage(DependencyObject child)
        {
            while (true)
            {
                var parent = VisualTreeHelper.GetParent(child);

                //CHeck if this is the end of the tree
                if (parent != null)
                {
                    if (parent is Page parentPage) return parentPage;
                    child = parent;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}