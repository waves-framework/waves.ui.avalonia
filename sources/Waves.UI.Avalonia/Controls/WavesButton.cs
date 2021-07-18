////using System;
////using System.ComponentModel;
////using Avalonia;
////using Avalonia.Controls;
////using Avalonia.Styling;
////using Waves.UI.Avalonia.Helpers;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Button.
////    /// </summary>
////    public class WavesButton : Button
////    {
////        /// <summary>
////        /// Defines <see cref="CornerRadius"/> property.
////        /// </summary>
////        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty = AvaloniaProperty.Register<Button, CornerRadius>(
////            nameof(CornerRadius),
////            new CornerRadius(3), 
////            OnCornerRadiusChangedCallback);

////        /// <summary>
////        /// Defines <see cref="IsAccent"/> property.
////        /// </summary>
////        public static readonly StyledProperty<bool> IsAccentProperty = AvaloniaProperty.Register<Button,bool>(
////            nameof(IsAccent),
////            false, 
////            OnIsAccentChangedCallback);

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
////        /// Gets or sets whether button is accent.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public bool IsAccent
////        {
////            get => (bool)GetValue(IsAccentProperty);
////            set => SetValue(IsAccentProperty, value);
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
////            if (d is not Button button)
////            {
////                return;
////            }

////            button.SetValue(ControlHelper.CornerRadiusProperty, e.NewValue);
////        }

////        /// <summary>
////        /// Callback when changed button's accent state.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnIsAccentChangedCallback(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            try
////            {
////                if (d is not Button button)
////                {
////                    return;
////                }

////                var isAccent = (bool)e.NewValue;
////                var style = isAccent
////                    ? (Style)button.TryFindResource("AccentButtonStyle")
////                    : (Style)button.TryFindResource("ButtonStyle");

////                button.Style = style;
////            }
////            catch (Exception exception)
////            {
////                Console.WriteLine(exception);
////            }
////        }
////    }
////}
