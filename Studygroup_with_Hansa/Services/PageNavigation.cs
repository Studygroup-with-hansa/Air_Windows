using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Studygroup_with_Hansa.Services
{
    class PageNavigation
    {
        public static Page FindParentPage(DependencyObject child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            //CHeck if this is the end of the tree
            if (parent == null) return null;

            Page parentPage = parent as Page;
            if (parentPage != null)
            {
                return parentPage;
            }
            else
            {
                //use recursion until it reaches a Window
                return FindParentPage(parent);
            }
        }
    }
}
