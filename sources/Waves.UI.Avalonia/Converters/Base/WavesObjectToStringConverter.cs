using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Waves.Core.Base.Interfaces;

namespace Waves.UI.Avalonia.Converters.Base
{
    /// <inheritdoc />
    public class WavesObjectToStringConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return value is not IWavesObject obj ? string.Empty : obj.ToString();
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
