using System.Net.Mime;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Waves.UI.Avalonia;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Showcase.Avalonia.Presentation.View.Windows;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Windows;

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
        public override async void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var service = await Core.GetInstanceAsync<IWavesNavigationService>();
                await service.NavigateAsync<MainWindowViewModel>();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
