using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Waves.Core.Base.Interfaces;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Attributes;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Windows;

namespace Waves.UI.Showcase.Avalonia.Presentation.View.Windows
{
    /// <summary>
    /// Main window.
    /// </summary>
    [WavesView(typeof(MainWindowViewModel))]
    public partial class MainWindow : WavesWindow
    {
        /// <inheritdoc />
        public MainWindow()
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
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
