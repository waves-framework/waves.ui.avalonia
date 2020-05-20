using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Fluid.UI.Avalonia.Showcase;
using ReactiveUI;

namespace Fluid.UI.Avalonia.Sandbox
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
            this.WhenActivated(disposables => { });
        }
    }
}
