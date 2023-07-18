using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Waves.UI.Avalonia.Converters.Base
{
    /// <inheritdoc />
    public class StringIsNullOrEmptyToBoolConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture)
        {
            var s = (string)value!;
            return string.IsNullOrEmpty(s);
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
