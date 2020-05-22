using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Fluid.UI.Avalonia.Showcase;
using Fluid.UI.Avalonia.Styles;
using ReactiveUI;

namespace Fluid.UI.Avalonia.Sandbox
{
    public class MainWindow : FluidWindow
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
