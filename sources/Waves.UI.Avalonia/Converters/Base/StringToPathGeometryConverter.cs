using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Waves.UI.Avalonia.Converters.Base
{
    /// <summary>
    /// Converts string to path geometry.
    /// </summary>
    public class StringToPathGeometryConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string) value;
            if (s == null) return null;

            try
            {
                var geometry = PathGeometry.Parse(s);
                return geometry;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}