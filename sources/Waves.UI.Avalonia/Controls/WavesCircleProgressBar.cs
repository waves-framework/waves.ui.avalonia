////using System.ComponentModel;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Waves circle progress bar.
////    /// </summary>
////    public class WavesCircleProgressBar : ProgressBar
////    {
////        /// <summary>
////        ///     Defines <see cref="Radius"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> RadiusProperty =
////            AvaloniaProperty.Register<,>Attached(
////                nameof(Radius),
////                typeof(double),
////                typeof(WavesCircleProgressBar),
////                new FrameworkPropertyMetadata(16.0d, FrameworkPropertyMetadataOptions.AffectsRender, OnRadiusChangedCallback));

////        /// <summary>
////        ///      Defines <see cref="Thickness"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> ThicknessProperty =
////            AvaloniaProperty.Register<,>Attached(
////                nameof(Thickness),
////                typeof(double),
////                typeof(WavesCircleProgressBar),
////                new FrameworkPropertyMetadata(3.0d, FrameworkPropertyMetadataOptions.AffectsRender, OnThicknessChangedCallback));

////        /// <summary>
////        /// Gets or sets radius.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public double Radius
////        {
////            get => (double)GetValue(RadiusProperty);
////            set => SetValue(RadiusProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets thickness.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public double Thickness
////        {
////            get => (double)GetValue(ThicknessProperty);
////            set => SetValue(ThicknessProperty, value);
////        }

////        /// <summary>
////        /// Callback when <see cref="Radius"/> changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnRadiusChangedCallback(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (d is not WavesCircleProgressBar progressBar)
////            {
////                return;
////            }

////            progressBar.SetValue(CircleProgressBarHelper.RadiusProperty, e.NewValue);
////        }

////        /// <summary>
////        /// Callback when <see cref="Thickness"/> changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnThicknessChangedCallback(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (d is not WavesCircleProgressBar progressBar)
////            {
////                return;
////            }

////            progressBar.SetValue(CircleProgressBarHelper.ThicknessProperty, e.NewValue);
////        }
////    }
////}
