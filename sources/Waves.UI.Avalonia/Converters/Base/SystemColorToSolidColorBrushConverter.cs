using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Waves.UI.Avalonia.Converters.Base
{
    /// <inheritdoc />
    public class SystemColorToSolidColorBrushConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var color = (Color)value;
            return new SolidColorBrush(color);
        }

        /// <inheritdoc />
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            var brush = (SolidColorBrush)value;
            return brush?.Color;
        }
    }
}
