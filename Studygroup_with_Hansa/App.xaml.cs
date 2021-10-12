using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Studygroup_with_Hansa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                forType: typeof(FrameworkElement),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: XmlLanguage.GetLanguage(ietfLanguageTag: CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}
