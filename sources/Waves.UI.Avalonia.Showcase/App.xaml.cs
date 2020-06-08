using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Waves.UI.Avalonia.Sandbox;

namespace Waves.UI.Avalonia.Showcase
{
    public class App : Application
    {
        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                var window = new MainWindow();
                window.DataContext = new MainWindowViewModel();
                desktopLifetime.MainWindow = window;
            }
            
            var core = new Core();
            core.Start(this);
            
            base.OnFrameworkInitializationCompleted();
        }
    }
}
