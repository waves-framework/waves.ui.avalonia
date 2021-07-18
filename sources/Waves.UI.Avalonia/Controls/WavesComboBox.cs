////using System.ComponentModel;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Waves ComboBox.
////    /// </summary>
////    public class WavesComboBox : ComboBox
////    {
////        /// <summary>
////        /// Defines corner radius property.
////        /// </summary>
////        public static readonly StyledProperty<> CornerRadiusProperty = AvaloniaProperty.Register<,>(
////            "CornerRadius",
////            typeof(CornerRadius),
////            typeof(WavesComboBox),
////            new FrameworkPropertyMetadata(new CornerRadius(3), FrameworkPropertyMetadataOptions.AffectsRender, OnCornerRadiusChangedCallback));

////        /// <summary>
////        ///     Defines <see cref="Label"/> dependency property.
////        /// </summary>
////        public static readonly StyledProperty<> LabelProperty =
////            AvaloniaProperty.Register<,>Attached(
////                "Label",
////                typeof(string),
////                typeof(WavesComboBox),
////                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender, OnLabelChangedCallback));

////        /// <summary>
////        ///     Defines <see cref="Watermark"/> dependency property.
////        /// </summary>
////        public static readonly StyledProperty<> WatermarkProperty =
////            AvaloniaProperty.Register<,>Attached(
////                "Watermark",
////                typeof(string),
////                typeof(WavesComboBox),
////                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender, OnWatermarkChangedCallback));

////        /// <summary>
////        /// Defines vector <see cref="Icon"/> dependency property.
////        /// </summary>
////        public static readonly StyledProperty<> IconProperty =
////            AvaloniaProperty.Register<,>Attached(
////                "Icon",
////                typeof(Geometry),
////                typeof(WavesComboBox),
////                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnIconChangedCallback));

////        /// <summary>
////        /// Gets or sets center.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public CornerRadius CornerRadius
////        {
////            get => (CornerRadius)GetValue(CornerRadiusProperty);
////            set => SetValue(CornerRadiusProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets ComboBox label.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public string Label
////        {
////            get => (string)GetValue(LabelProperty);
////            set => SetValue(LabelProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets ComboBox watermark.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public string Watermark
////        {
////            get => (string)GetValue(WatermarkProperty);
////            set => SetValue(WatermarkProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets ComboBox icon.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public Geometry Icon
////        {
////            get => (Geometry)GetValue(IconProperty);
////            set => SetValue(IconProperty, value);
////        }

////        /// <summary>
////        /// Callback when corner radius changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnCornerRadiusChangedCallback(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (d is not WavesComboBox comboBox)
////            {
////                return;
////            }

////            comboBox.SetValue(ControlHelper.CornerRadiusProperty, e.NewValue);
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
////            if (d is not WavesComboBox comboBox)
////            {
////                return;
////            }

////            comboBox.SetValue(TextBoxHelper.LabelProperty, e.NewValue);
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
////            if (d is not WavesComboBox comboBox)
////            {
////                return;
////            }

////            comboBox.SetValue(TextBoxHelper.WatermarkProperty, e.NewValue);
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
////            if (d is not WavesComboBox comboBox)
////            {
////                return;
////            }

////            comboBox.SetValue(TextBoxHelper.IconProperty, e.NewValue);
////        }
////    }
////}
