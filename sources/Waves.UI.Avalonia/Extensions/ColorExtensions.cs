namespace Waves.UI.Avalonia.Extensions
{
    /// <summary>
    /// Color extensions.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts <see cref="System.Drawing.Color"/> to <see cref="System.Windows.Media.Color"/>.
        /// </summary>
        /// <param name="color">Value of <see cref="System.Drawing.Color"/>.</param>
        /// <returns>Returns value of <see cref="System.Windows.Media.Color"/>.</returns>
        public static global::Avalonia.Media.Color ToSystemColor(this System.Drawing.Color color)
        {
            return new global::Avalonia.Media.Color(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Converts color to HEX string.
        /// </summary>
        /// <param name="color">Color.</param>
        /// <returns>Returns HEX string.</returns>
        public static string ToHexString(
            this global::Avalonia.Media.Color color)
        {
            var a = color.A;
            var r = color.R;
            var g = color.G;
            var b = color.B;
            var isHexPrefix = a != 255;

            return isHexPrefix ? $"#{a:X2}{r:X2}{g:X2}{b:X2}" : $"#{r:X2}{g:X2}{b:X2}";
        }
    }
}
