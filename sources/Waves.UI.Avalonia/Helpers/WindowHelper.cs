// using System.Windows.Input;
//
// namespace Waves.UI.Avalonia.Helpers
// {
//     /// <summary>
//     /// Window's dependency property helper.
//     /// </summary>
//     public static class WindowHelper
//     {
//         /// <summary>
//         ///     Defines "FrontLayerContent" dependency property.
//         /// </summary>
//         public static readonly StyledProperty<CornerRadius> FrontLayerContentProperty =
//             AvaloniaProperty.Register(
//                 "FrontLayerContent",
//                 typeof(UIElement),
//                 typeof(WindowHelper),
//                 new UIPropertyMetadata(null));
//
//         /// <summary>
//         ///     Defines "CanGoBack" dependency property.
//         /// </summary>
//         public static readonly StyledProperty<CornerRadius> CanGoBackProperty =
//             AvaloniaProperty.Register(
//                 "CanGoBack",
//                 typeof(bool),
//                 typeof(WindowHelper),
//                 new FrameworkPropertyMetadata(false));
//
//         /// <summary>
//         ///     Defines "GoBackCommand" dependency property.
//         /// </summary>
//         public static readonly StyledProperty<CornerRadius> GoBackCommandProperty =
//             AvaloniaProperty.Register(
//                 "GoBackCommand",
//                 typeof(ICommand),
//                 typeof(WindowHelper),
//                 new FrameworkPropertyMetadata(null));
//
//         /// <summary>
//         ///     Gets front layer content.
//         /// </summary>
//         /// <param name="obj">Dependency object.</param>
//         /// <returns>Description value.</returns>
//         public static UIElement GetFrontLayerContent(DependencyObject obj)
//         {
//             return (UIElement)obj.GetValue(FrontLayerContentProperty);
//         }
//
//         /// <summary>
//         ///     Sets front layer content.
//         /// </summary>
//         /// <param name="obj">Dependency object.</param>
//         /// <param name="value">Description value.</param>
//         public static void SetFrontLayerContent(DependencyObject obj, UIElement value)
//         {
//             obj.SetValue(FrontLayerContentProperty, value);
//         }
//
//         /// <summary>
//         ///     Gets "CanGoBack".
//         /// </summary>
//         /// <param name="obj">Dependency object.</param>
//         /// <returns>Description value.</returns>
//         public static bool GetCanGoBack(DependencyObject obj)
//         {
//             return (bool)obj.GetValue(CanGoBackProperty);
//         }
//
//         /// <summary>
//         ///     Sets "CanGoBack".
//         /// </summary>
//         /// <param name="obj">Dependency object.</param>
//         /// <param name="value">Description value.</param>
//         public static void SetCanGoBack(DependencyObject obj, bool value)
//         {
//             obj.SetValue(CanGoBackProperty, value);
//         }
//
//         /// <summary>
//         ///     Gets "GetBackCommand".
//         /// </summary>
//         /// <param name="obj">Dependency object.</param>
//         /// <returns>Description value.</returns>
//         public static ICommand GetGoBackCommand(DependencyObject obj)
//         {
//             return (ICommand)obj.GetValue(GoBackCommandProperty);
//         }
//
//         /// <summary>
//         ///     Sets "GetBackCommand".
//         /// </summary>
//         /// <param name="obj">Dependency object.</param>
//         /// <param name="value">Description value.</param>
//         public static void SetGoBackCommand(DependencyObject obj, ICommand value)
//         {
//             obj.SetValue(GoBackCommandProperty, value);
//         }
//     }
// }
