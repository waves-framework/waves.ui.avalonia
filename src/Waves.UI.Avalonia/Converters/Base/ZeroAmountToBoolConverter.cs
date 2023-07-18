using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Waves.UI.Avalonia.Converters.Base
{
    /// <inheritdoc />
    public class ZeroAmountToBoolConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture)
        {
            return value != null && (int)value == 0;
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
