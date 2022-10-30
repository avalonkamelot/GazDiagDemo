using System.Windows;

namespace TubeDemo.Controls.InfoGrid
{
    public class InfoGridAttachedProperties
    {
        public static bool GetIsFocussed(DependencyObject obj) => (bool)obj.GetValue(IsFocussedProperty);

        public static void SetIsFocussed(DependencyObject obj, bool value) => obj.SetValue(IsFocussedProperty, value);

        public static readonly DependencyProperty IsFocussedProperty =
            DependencyProperty.RegisterAttached("IsFocussed", typeof(bool), typeof(InfoGridAttachedProperties), new UIPropertyMetadata(false));
    }
}
