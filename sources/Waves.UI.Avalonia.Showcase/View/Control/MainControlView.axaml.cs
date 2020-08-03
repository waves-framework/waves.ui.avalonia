using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Waves.UI.Avalonia.Showcase.View.Control
{
    public class MainControlView : UserControl
    {
        public MainControlView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
