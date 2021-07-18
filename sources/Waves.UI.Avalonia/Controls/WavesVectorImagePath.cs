////using System.ComponentModel;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Vector image path.
////    /// </summary>
////    [ContentProperty(nameof(Value))]
////    [DefaultProperty(nameof(Value))]
////    public class WavesVectorImagePath : FrameworkElement
////    {
////        /// <summary>
////        ///     Defines <see cref="Value" /> property.
////        /// </summary>
////        public static readonly StyledProperty<> ValueProperty = AvaloniaProperty.Register<,>(
////            nameof(Value),
////            typeof(string),
////            typeof(WavesVectorImagePath),
////            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

////        /// <summary>
////        ///     Defines <see cref="Fill" /> property.
////        /// </summary>
////        public static readonly StyledProperty<> FillProperty = AvaloniaProperty.Register<,>(
////            nameof(Fill),
////            typeof(Color?),
////            typeof(WavesVectorImagePath),
////            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

////        /// <summary>
////        ///     Gets or sets value.
////        /// </summary>
////        [Category("Waves.UI SDK - Path")]
////        public string Value
////        {
////            get => (string)GetValue(ValueProperty);
////            set => SetValue(ValueProperty, value);
////        }

////        /// <summary>
////        ///     Gets or sets fill color.
////        /// </summary>
////        [Category("Waves.UI SDK - Path")]
////        public Color? Fill
////        {
////            get => (Color?)GetValue(FillProperty);
////            set => SetValue(FillProperty, value);
////        }
////    }
////}
