using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace Waves.UI.Avalonia.Converters.Base
{
    /// <summary>
    /// Converts number to thickness.
    /// </summary>
    public class ValueToThicknessConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = System.Convert.ToDouble(value);
            return new Thickness(v);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}