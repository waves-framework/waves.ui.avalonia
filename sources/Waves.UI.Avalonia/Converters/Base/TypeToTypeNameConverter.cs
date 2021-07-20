using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Waves.Core.Extensions;

namespace Waves.UI.Avalonia.Converters.Base
{
    /// <inheritdoc />
    public class TypeToTypeNameConverter : IValueConverter
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
                return "Unknown";
            }

            return value is not Type type ? "Unknown" : type.GetFriendlyName();
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
