namespace Waves.UI.Avalonia.Extensions
{
    /// <summary>
    /// Text alignment.
    /// </summary>
    public static class TextAlignment
    {
        /// <summary>
        /// Converts Waves text alignment to system text alignment.
        /// </summary>
        /// <param name="alignment">Waves text alignment.</param>
        /// <returns>System text alignment.</returns>
        public static global::Avalonia.Media.TextAlignment ToSystemTextAlignment(this Drawing.Base.Enums.TextAlignment alignment)
        {
            switch (alignment)
            {
                case Drawing.Base.Enums.TextAlignment.Left:
                    return global::Avalonia.Media.TextAlignment.Left;
                case Drawing.Base.Enums.TextAlignment.Right:
                    return global::Avalonia.Media.TextAlignment.Right;
                case Drawing.Base.Enums.TextAlignment.Center:
                    return global::Avalonia.Media.TextAlignment.Center;
                default:
                    return global::Avalonia.Media.TextAlignment.Left;
            }
        }
    }
}