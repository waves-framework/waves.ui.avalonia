using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Waves.UI.Avalonia;
using Waves.UI.Services.Interfaces;
using Waves.UI.Showcase.Avalonia.Presentation.View.Windows;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Pages;
using Waves.UI.Showcase.Common.Presentation.ViewModel.UserControls;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Windows;

namespace Waves.UI.Showcase.Avalonia.App
{
    /// <summary>
    /// App.
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
                await NavigationService.NavigateAsync<MainWindowViewModel>();
                await NavigationService.NavigateAsync<MainPageViewModel>();
                await NavigationService.NavigateAsync<TestUserControlViewModel>();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
