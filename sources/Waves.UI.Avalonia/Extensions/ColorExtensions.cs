using Avalonia.Media;

namespace Waves.UI.Avalonia.Extensions
{
    /// <summary>
    ///     Color extensions.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        ///     Converts System.Windows.Media.Color to Waves.Core.Base.Color.
        /// </summary>
        /// <param name="color">Instance of System.Windows.Media.Color.</param>
        /// <returns>New instance of Waves.Core.Base.Color.</returns>
        public static Waves.Core.Base.Color ToWavesColor(this Color color)
        {
            return new Waves.Core.Base.Color(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        ///     Converts System.Windows.Media.Color to Waves.Core.Base.Color.
        /// </summary>
        /// <param name="color">Instance of System.Windows.Media.Color.</param>
        /// <returns>New instance of Waves.Core.Base.Color.</returns>
        public static Color ToSystemColor(this Waves.Core.Base.Color color)
        {
            return new Color(color.A,color.R,color.G,color.B);
        }
    }
}