using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Waves.UI.Avalonia.Converters.Base
{
    /// <summary>
    /// Converts <see cref="System.Drawing.Color"/> to <see cref="Color"/> with zero alpha channel.
    /// </summary>
    public class ColorToTransparentColorConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var color = (System.Drawing.Color)value;
            return new Color(0, color.R, color.G, color.B);
        }

        /// <inheritdoc />
        public object? ConvertBack(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture)
        {
            return null;
        }
    }
}
