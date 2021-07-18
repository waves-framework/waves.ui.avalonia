using System.Net.Mime;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Waves.UI.Avalonia;
using Waves.UI.Showcase.Avalonia.Presentation.View.Windows;

namespace Waves.UI.Showcase.Avalonia.App
{
    /// <summary>
    /// Application.
    /// </summary>
    public class App : WavesApplication
    {
        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();
            AvaloniaXamlLoader.Load(this);
        }

        /// <inheritdoc />
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
