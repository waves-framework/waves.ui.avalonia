using Avalonia;
using Avalonia.Controls;

namespace Waves.UI.Avalonia.Helpers
{
    /// <summary>
    ///     TextBox's dependency property helper.
    /// </summary>
    public static class TextBoxHelper
    {
        /// <summary>
        ///     Defines "Label" dependency property.
        /// </summary>
        public static readonly StyledProperty<string> LabelProperty =
            AvaloniaProperty.Register<TextBox, string>(
                "Label",
                string.Empty);

        /// <summary>
        ///     Defines "Watermark" dependency property.
        /// </summary>
        public static readonly StyledProperty<string> WatermarkProperty =
            AvaloniaProperty.Register<TextBox, string>(
                "Watermark",
                string.Empty);

        /// <summary>
        ///     Gets watermark.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>Description value.</returns>
        public static string GetLabel(StyledElement obj)
        {
            return obj.GetValue(LabelProperty);
        }

        /// <summary>
        ///     Sets watermark.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">Description value.</param>
        public static void SetLabel(StyledElement obj, string value)
        {
            obj.SetValue(LabelProperty, value);
        }

        /// <summary>
        ///     Gets watermark.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>Description value.</returns>
        public static string GetWatermark(StyledElement obj)
        {
            return obj.GetValue(WatermarkProperty);
        }

        /// <summary>
        ///     Sets watermark.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">Description value.</param>
        public static void SetWatermark(StyledElement obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }
    }
}