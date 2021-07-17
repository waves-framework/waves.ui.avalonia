using Avalonia;
using Avalonia.Controls;

namespace Waves.UI.Avalonia.Helpers
{
    /// <summary>
    ///     Tab control dependency property helper.
    /// </summary>
    public static class TabControlHelper
    {
        /// <summary>
        ///     Gets or sets "Tab Width" dependency property.
        /// </summary>
        public static readonly StyledProperty<double> TabWidthProperty =
            AvaloniaProperty.Register<TabControl, double>(
                "TabWidth",
                double.NaN);

        /// <summary>
        /// Gets or sets "IsContentVisible" dependecy property.
        /// </summary>
        public static readonly StyledProperty<bool> IsContentVisibleProperty =
            AvaloniaProperty.Register<TabControl, bool>(
                "IsContentVisible",
                true);

        /// <summary>
        ///     Gets tab width.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>Tab width value.</returns>
        public static double GetTabWidth(StyledElement obj)
        {
            return (double)obj.GetValue(TabWidthProperty);
        }

        /// <summary>
        ///     Gets whether is content visible.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>Returns whether in content visible value.</returns>
        public static bool GetIsContentVisible(StyledElement obj)
        {
            return (bool)obj.GetValue(IsContentVisibleProperty);
        }

        /// <summary>
        ///     Sets tab width.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">Tab width value.</param>
        public static void SetTabWidth(StyledElement obj, double value)
        {
            obj.SetValue(TabWidthProperty, value);
        }

        /// <summary>
        ///     Sets whether is content visible.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">Is content visible value.</param>
        public static void SetIsContentVisible(StyledElement obj, bool value)
        {
            obj.SetValue(IsContentVisibleProperty, value);
        }
    }
}
