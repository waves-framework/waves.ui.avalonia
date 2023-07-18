using System.ComponentModel;
using Avalonia;
using Avalonia.Media;

namespace Waves.UI.Avalonia.Controls
{
    /// <summary>
    /// Vector image path.
    /// </summary>
    [DefaultProperty(nameof(Value))]
    public class WavesVectorImagePath : StyledElement
    {
        /// <summary>
        ///     Defines <see cref="Value" /> property.
        /// </summary>
        public static readonly StyledProperty<string> ValueProperty = AvaloniaProperty.Register<WavesVectorImagePath, string>(
            nameof(Value));

        /// <summary>
        ///     Defines <see cref="Fill" /> property.
        /// </summary>
        public static readonly StyledProperty<Color?> FillProperty = AvaloniaProperty.Register<WavesVectorImagePath, Color?>(
            nameof(Fill));

        /// <summary>
        ///     Gets or sets value.
        /// </summary>
        [Category("Waves.UI SDK - Path")]
        public string Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        ///     Gets or sets fill color.
        /// </summary>
        [Category("Waves.UI SDK - Path")]
        public Color? Fill
        {
            get => GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }
    }
}
