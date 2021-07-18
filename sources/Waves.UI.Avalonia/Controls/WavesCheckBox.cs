////using System.ComponentModel;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Waves CheckBox control.
////    /// </summary>
////    public class WavesCheckBox : CheckBox
////    {
////        /// <summary>
////        /// Defines <see cref="CornerRadius"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> CornerRadiusProperty = AvaloniaProperty.Register<,>(
////            "CornerRadius",
////            typeof(CornerRadius),
////            typeof(WavesCheckBox),
////            new FrameworkPropertyMetadata(new CornerRadius(3), FrameworkPropertyMetadataOptions.AffectsRender, OnCornerRadiusChangedCallback));

////        /// <summary>
////        /// Gets or sets corner radius.
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
////            if (!(d is WavesCheckBox checkBox))
////            {
////                return;
////            }

////            checkBox.SetValue(ControlHelper.CornerRadiusProperty, e.NewValue);
////        }
////    }
////}
