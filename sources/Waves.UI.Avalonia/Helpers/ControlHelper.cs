using Avalonia;
using Avalonia.Controls;

namespace Waves.UI.Avalonia.Helpers
{
    /// <summary>
    ///     Control's dependency property helper.
    /// </summary>
    public static class ControlHelper
    {
        /// <summary>
        ///     Gets or sets control's "Corner Radius" property.
        /// </summary>
        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<Control, CornerRadius>(
                "CornerRadius",
                new CornerRadius(0, 0, 0, 0));

        /// <summary>
        ///     Gets or sets control's "Region" property.
        /// </summary>
        public static readonly StyledProperty<string> RegionProperty =
            AvaloniaProperty.Register<Control, string>(
                "Region",
                string.Empty);

        /// <summary>
        ///     Gets control's "Corner Radius".
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>Corner radius.</returns>
        public static CornerRadius GetCornerRadius(StyledElement obj)
        {
            return obj.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        ///     Sets control's "Corner Radius" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">Corner radius.</param>
        public static void SetCornerRadius(StyledElement obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        ///     Gets control's "Region" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>Region value.</returns>
        public static string GetRegion(StyledElement obj)
        {
            return obj.GetValue(RegionProperty);
        }

        /// <summary>
        ///     Sets control's "Region" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">Region value.</param>
        public static void SetRegion(StyledElement obj, string value)
        {
            obj.SetValue(RegionProperty, value);
        }
    }
}