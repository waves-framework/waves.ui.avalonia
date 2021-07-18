////using System.ComponentModel;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Waves content control.
////    /// </summary>
////    public class WavesContentControl : ContentControl
////    {
////        /// <summary>
////        /// Defines <see cref="Region"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> RegionProperty = AvaloniaProperty.Register<,>(
////            nameof(Region),
////            typeof(string),
////            typeof(WavesContentControl),
////            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

////        /// <summary>
////        /// Defines corner radius property.
////        /// </summary>
////        public static readonly StyledProperty<> CornerRadiusProperty = AvaloniaProperty.Register<,>(
////            "CornerRadius",
////            typeof(CornerRadius),
////            typeof(WavesContentControl),
////            new FrameworkPropertyMetadata(new CornerRadius(3), FrameworkPropertyMetadataOptions.AffectsRender, OnCornerRadiusChangedCallback));

////        /// <summary>
////        /// Gets or sets center.
////        /// </summary>
////        [Category("Waves.UI SDK - Content")]
////        public string Region
////        {
////            get => (string)GetValue(RegionProperty);
////            set => SetValue(RegionProperty, value);
////        }

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
////        /// Callback when corner radius changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnCornerRadiusChangedCallback(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (!(d is WavesContentControl control))
////            {
////                return;
////            }

////            control.SetValue(ControlHelper.CornerRadiusProperty, e.NewValue);
////        }
////    }
////}
