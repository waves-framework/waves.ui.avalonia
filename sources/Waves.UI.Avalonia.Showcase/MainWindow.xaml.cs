using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Waves.UI.Avalonia.Showcase;
using Waves.UI.Avalonia.Styles;
using ReactiveUI;

namespace Waves.UI.Avalonia.Sandbox
{
    public class MainWindow : WavesWindow
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
