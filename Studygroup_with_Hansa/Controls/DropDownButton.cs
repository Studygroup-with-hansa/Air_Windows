using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Studygroup_with_Hansa.Controls
{
    public class DropDownButton : ToggleButton
    {
        public static readonly DependencyProperty MenuProperty = DependencyProperty.Register(name: "Menu",
            propertyType: typeof(ContextMenu), ownerType: typeof(DropDownButton), typeMetadata: new UIPropertyMetadata(defaultValue: null, propertyChangedCallback: OnMenuChanged));

        public DropDownButton()
        {
            // Bind the ToogleButton.IsChecked property to the drop-down's IsOpen property
            var binding = new Binding(path: "Menu.IsOpen")
            {
                Source = this
            };
            _ = SetBinding(dp: IsCheckedProperty, binding: binding);
            DataContextChanged += (sender, args) =>
            {
                if (Menu != null)
                    Menu.DataContext = DataContext;
            };
        }

        // ***Properties***
        public ContextMenu Menu
        {
            get => (ContextMenu) GetValue(dp: MenuProperty);
            set => SetValue(dp: MenuProperty, value: value);
        }

        private static void OnMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dropDownButton = (DropDownButton)d;
            var contextMenu = (ContextMenu)e.NewValue;
            contextMenu.DataContext = dropDownButton.DataContext;
        }

        protected override void OnClick()
        {
            if (Menu == null) return;
            Menu.PlacementTarget = this;
            Menu.Placement = PlacementMode.Bottom;
            Menu.IsOpen = true;
        }
    }
}