using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;

namespace Waves.UI.Avalonia.Helpers
{
    /// <summary>
    ///     Window's dependency property helper.
    /// </summary>
    public static class WindowHelper
    {
        /// <summary>
        ///     Defines "FrontLayerContent" dependency property.
        /// </summary>
        public static readonly StyledProperty<StyledElement> FrontLayerContentProperty =
            AvaloniaProperty.Register<Window, StyledElement>(
                "FrontLayerContent");

        /// <summary>
        ///     Defines "CanGoBack" dependency property.
        /// </summary>
        public static readonly StyledProperty<bool> CanGoBackProperty =
            AvaloniaProperty.Register<Window, bool>(
                "CanGoBack");

        /// <summary>
        ///     Defines "GoBackCommand" dependency property.
        /// </summary>
        public static readonly StyledProperty<ICommand> GoBackCommandProperty =
            AvaloniaProperty.Register<Window, ICommand>(
                "GoBackCommand");

        /// <summary>
        ///     Gets front layer content.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>Description value.</returns>
        public static StyledElement GetFrontLayerContent(
            StyledElement obj)
        {
            return obj.GetValue(FrontLayerContentProperty);
        }

        /// <summary>
        ///     Sets front layer content.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">Description value.</param>
        public static void SetFrontLayerContent(
            StyledElement obj,
            StyledElement value)
        {
            obj.SetValue(FrontLayerContentProperty, value);
        }

        /// <summary>
        ///     Gets "CanGoBack".
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>Description value.</returns>
        public static bool GetCanGoBack(
            StyledElement obj)
        {
            return obj.GetValue(CanGoBackProperty);
        }

        /// <summary>
        ///     Sets "CanGoBack".
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">Description value.</param>
        public static void SetCanGoBack(
            StyledElement obj,
            bool value)
        {
            obj.SetValue(CanGoBackProperty, value);
        }

        /// <summary>
        ///     Gets "GetBackCommand".
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>Description value.</returns>
        public static ICommand GetGoBackCommand(
            StyledElement obj)
        {
            return obj.GetValue(GoBackCommandProperty);
        }

        /// <summary>
        ///     Sets "GetBackCommand".
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">Description value.</param>
        public static void SetGoBackCommand(
            StyledElement obj,
            ICommand value)
        {
            obj.SetValue(GoBackCommandProperty, value);
        }
    }
}