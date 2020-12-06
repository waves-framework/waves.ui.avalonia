using System;
using Avalonia;
using Avalonia.Dialogs;
using Avalonia.ReactiveUI;

namespace Waves.UI.Avalonia.Showcase
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        private static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .With(new X11PlatformOptions {EnableMultiTouch = true, UseDBusMenu = true})
                .With(new Win32PlatformOptions {EnableMultitouch = true, AllowEglInitialization = true})
                .With(new AvaloniaNativePlatformOptions { UseGpu = true })
                .UseSkia()
                .UseReactiveUI()
                .UseManagedSystemDialogs();
        }
    }
}
