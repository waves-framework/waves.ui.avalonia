using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Waves.UI.Avalonia.Styles;

namespace Waves.UI.Avalonia.Showcase.View
{
    /// <summary>
    /// Main window.
    /// </summary>
    public class MainWindowView : Window
    {
        /// <summary>
        /// Creates new instance of <see cref="MainWindowView"/>.
        /// </summary>
        public MainWindowView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes components.
        /// </summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}