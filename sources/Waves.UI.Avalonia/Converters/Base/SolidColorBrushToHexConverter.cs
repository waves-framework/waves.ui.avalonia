using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Waves.UI.Avalonia.Converters.Base
{
    /// <inheritdoc />
    public class SolidColorBrushToHexConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (!(value is SolidColorBrush brush))
            {
                return null;
            }

            var color = brush.Color;
            var a = color.A;
            var r = color.R;
            var g = color.G;
            var b = color.B;
            var isHexPrefix = a != 255;

            return isHexPrefix ? $"#{a:X2}{r:X2}{g:X2}{b:X2}" : $"#{r:X2}{g:X2}{b:X2}";
        }

        /// <inheritdoc />
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return null;
        }
    }
}
