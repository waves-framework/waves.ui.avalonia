////using System.ComponentModel;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Waves TextBox.
////    /// </summary>
////    public class WavesTextBox : TextBox
////    {
////        /// <summary>
////        ///     Defines <see cref="Label"/> dependency property.
////        /// </summary>
////        public static readonly StyledProperty<> LabelProperty =
////            AvaloniaProperty.Register<,>Attached(
////                "Label",
////                typeof(string),
////                typeof(WavesTextBox),
////                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender, OnLabelChangedCallback));

////        /// <summary>
////        ///     Defines <see cref="Watermark"/> dependency property.
////        /// </summary>
////        public static readonly StyledProperty<> WatermarkProperty =
////            AvaloniaProperty.Register<,>Attached(
////                "Watermark",
////                typeof(string),
////                typeof(WavesTextBox),
////                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender, OnWatermarkChangedCallback));

////        /// <summary>
////        /// Defines vector <see cref="Icon"/> dependency property.
////        /// </summary>
////        public static readonly StyledProperty<> IconProperty =
////            AvaloniaProperty.Register<,>Attached(
////                "Icon",
////                typeof(Geometry),
////                typeof(WavesTextBox),
////                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnIconChangedCallback));

////        /// <summary>
////        /// Gets or sets TextBox label.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public string Label
////        {
////            get => (string)GetValue(LabelProperty);
////            set => SetValue(LabelProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets TextBox watermark.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public string Watermark
////        {
////            get => (string)GetValue(WatermarkProperty);
////            set => SetValue(WatermarkProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets TextBox icon.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public Geometry Icon
////        {
////            get => (Geometry)GetValue(IconProperty);
////            set => SetValue(IconProperty, value);
////        }

////        /// <summary>
////        /// Callback when label changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnLabelChangedCallback(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (!(d is WavesTextBox textBox))
////            {
////                return;
////            }

////            textBox.SetValue(TextBoxHelper.LabelProperty, e.NewValue);
////        }

////        /// <summary>
////        /// Callback when watermark changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnWatermarkChangedCallback(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (!(d is WavesTextBox textBox))
////            {
////                return;
////            }

////            textBox.SetValue(TextBoxHelper.WatermarkProperty, e.NewValue);
////        }

////        /// <summary>
////        /// Callback when icon changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnIconChangedCallback(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (!(d is WavesTextBox textBox))
////            {
////                return;
////            }

////            textBox.SetValue(TextBoxHelper.IconProperty, e.NewValue);
////        }
////    }
////}
