using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Waves.Core.Base.Interfaces;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Plugins.Services.Interfaces;

namespace Waves.UI.Showcase.Avalonia.Presentation.View.Windows
{
    /// <summary>
    /// Main window.
    /// </summary>
    public partial class MainWindow : WavesWindow
    {
        /// <inheritdoc />
        public MainWindow() : base()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        public MainWindow(
            IWavesCore core,
            IWavesNavigationService navigationService) : base(core, navigationService)
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Initializes components.
        /// </summary>
        protected void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
