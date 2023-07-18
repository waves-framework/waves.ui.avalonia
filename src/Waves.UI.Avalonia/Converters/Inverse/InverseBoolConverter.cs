using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Waves.UI.Avalonia.Converters.Inverse
{
    /// <inheritdoc />
    public class InverseBoolConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture)
        {
            return value != null && !(bool)value;
        }

        /// <inheritdoc />
        public object ConvertBack(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture)
        {
            return value != null && !(bool)value;
        }
    }
}
