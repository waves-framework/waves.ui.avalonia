using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Waves.Core.Base.Interfaces;
using Waves.UI.Avalonia.Showcase.Presentation.Controllers;
using Waves.UI.Avalonia.Showcase.View;

namespace Waves.UI.Avalonia.Showcase
{
    /// <summary>
    /// Application.
    /// </summary>
    public class App : Application
    {
        private Window _window;
        
        /// <summary>
        ///     Gets UI Core.
        /// </summary>
        public static Core Core { get; } = new Core();

        /// <summary>
        /// Initializes application.
        /// </summary>
        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        /// <summary>
        /// Actions when framework initialized.
        /// </summary>
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                var controller = new MainTabPresentationController(Core);

                _window = new MainWindowView {DataContext = controller};
                desktopLifetime.MainWindow = _window;

                Core.Start(this, _window);

                controller.MessageReceived += OnControllerMessageReceived;
                controller.Initialize();

                Core.AttachMainWindow(_window);

                _window.KeyDown += OnKeyDown;
                _window.Closing += OnViewClosing;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
#if DEBUG
            if (e.KeyModifiers == KeyModifiers.Shift)
            {
                // Render Debug information
                if (e.Key == Key.D)
                {
                    _window.Renderer.DrawFps = !_window.Renderer.DrawFps;
                    _window.Renderer.DrawDirtyRects = !_window.Renderer.DrawDirtyRects;
                }
            }
#endif
        }

        /// <summary>
        ///     Actions when controller's message received.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Message.</param>
        private void OnControllerMessageReceived(object sender, IWavesMessage e)
        {
            Core.WriteLog(e);
        }

        /// <summary>
        ///     Actions when main view closing.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void OnViewClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Core.Stop();
        }
    }
}
