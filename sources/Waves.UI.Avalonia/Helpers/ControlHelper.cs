using Avalonia;
using Avalonia.Controls;

namespace Waves.UI.Avalonia.Helpers
{
    /// <summary>
    /// Control helper.
    /// </summary>
    public static class ControlHelper
    {
        /// <summary>
        /// Defines corner radius property for controls.
        /// </summary>
        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<Control, CornerRadius>(
                "CornerRadius", 
                new CornerRadius(0, 0, 0, 0));

        /// <summary>
        ///     Gets control's "Corner Radius".
        /// </summary>
        /// <param name="element">Styled element..</param>
        /// <returns>Corner radius.</returns>
        public static CornerRadius GetCornerRadius(StyledElement element)
        {
            return (CornerRadius) element.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        ///     Sets control's "Corner Radius" property.
        /// </summary>
        /// <param name="element">Styled element.</param>
        /// <param name="value">Corner radius.</param>
        public static void SetCornerRadius(StyledElement element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }
    }
}