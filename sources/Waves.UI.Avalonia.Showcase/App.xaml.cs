using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
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

                var window = new MainWindowView {DataContext = controller};
                desktopLifetime.MainWindow = window;

                Core.Start(this, window);

                controller.MessageReceived += OnControllerMessageReceived;
                controller.Initialize();

                Core.AttachMainWindow(window);

                window.Closing += OnViewClosing;

                Core.AddMessageSeparator();
            }

            base.OnFrameworkInitializationCompleted();
        }

        /// <summary>
        ///     Actions when controller's message received.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Message.</param>
        private void OnControllerMessageReceived(object sender, IMessage e)
        {
            Core.WriteLogMessage(e);
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
