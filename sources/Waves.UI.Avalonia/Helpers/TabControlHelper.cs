using Avalonia;
using Avalonia.Controls;

namespace Waves.UI.Avalonia.Helpers
{
    /// <summary>
    /// Helper for TabControl.
    /// </summary>
    public static class TabControlHelper
    {
        /// <summary>
        /// Defines property whether is tab control's content visible.
        /// </summary>
        public static readonly StyledProperty<bool> IsContentVisibleProperty =
            AvaloniaProperty.Register<Control, bool>(
                "IsContentVisible", 
                true);
        
        /// <summary>
        ///     Gets control's "IsContentVisible".
        /// </summary>
        /// <param name="element">Styled element.</param>
        /// <returns>Whether is tab control content visible..</returns>
        public static bool GetIsContentVisible(StyledElement element)
        {
            return element.GetValue(IsContentVisibleProperty);
        }

        /// <summary>
        ///     Sets control's "IsContentVisible" property.
        /// </summary>
        /// <param name="element">Styled element.</param>
        /// <param name="value">Whether is tab control content visible.</param>
        public static void SetIsContentVisible(StyledElement element, bool value)
        {
            element.SetValue(IsContentVisibleProperty, value);
        }

    }
}