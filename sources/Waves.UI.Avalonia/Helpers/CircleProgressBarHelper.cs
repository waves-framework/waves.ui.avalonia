// namespace Waves.UI.Avalonia.Helpers
// {
//     /// <summary>
//     /// Circle progress bar helper.
//     /// </summary>
//     public static class CircleProgressBarHelper
//     {
//         /// <summary>
//         ///     Gets or sets control's "Radius" property.
//         /// </summary>
//         public static readonly StyledProperty<double> RadiusProperty =
//             AvaloniaProperty.Register<>(
//                 "Radius",
//                 typeof(double),
//                 typeof(CircleProgressBarHelper),
//                 new PropertyMetadata(16.0d));
//
//         /// <summary>
//         ///     Gets or sets control's "Thickness" property.
//         /// </summary>
//         public static readonly StyledProperty<CornerRadius> ThicknessProperty =
//             AvaloniaProperty.Register(
//                 "Thickness",
//                 typeof(double),
//                 typeof(CircleProgressBarHelper),
//                 new PropertyMetadata(3.0d));
//
//         /// <summary>
//         ///     Gets control's "Radius".
//         /// </summary>
//         /// <param name="obj">Dependency object.</param>
//         /// <returns>Radius.</returns>
//         public static double GetRadius(DependencyObject obj)
//         {
//             return (double)obj.GetValue(RadiusProperty);
//         }
//
//         /// <summary>
//         ///     Sets control's "Radius" property.
//         /// </summary>
//         /// <param name="obj">Dependency object.</param>
//         /// <param name="value">Radius.</param>
//         public static void SetRadius(DependencyObject obj, double value)
//         {
//             obj.SetValue(RadiusProperty, value);
//         }
//
//         /// <summary>
//         ///     Gets control's "Thickness".
//         /// </summary>
//         /// <param name="obj">Dependency object.</param>
//         /// <returns>Thickness.</returns>
//         public static double GetThickness(DependencyObject obj)
//         {
//             return (double)obj.GetValue(ThicknessProperty);
//         }
//
//         /// <summary>
//         ///     Sets control's "Thickness" property.
//         /// </summary>
//         /// <param name="obj">Dependency object.</param>
//         /// <param name="value">Thickness.</param>
//         public static void SetThickness(DependencyObject obj, double value)
//         {
//             obj.SetValue(ThicknessProperty, value);
//         }
//     }
// }
