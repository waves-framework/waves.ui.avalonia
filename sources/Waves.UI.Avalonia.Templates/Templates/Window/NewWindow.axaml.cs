using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Microsoft.Extensions.Logging;
using Waves.Trading.Client.Desktop.ViewModel.Window;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Templates.Window.Namespace
{
    /// <summary>
    /// Main window.
    /// </summary>
    [WavesView(typeof(NewWindowViewModel))]
    public partial class NewWindow : WavesWindow
    {
        /// <summary>
        /// Creates new instance os <see cref="NewWindow"/>.
        /// </summary>
        public NewWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        /// <summary>
        /// Creates new instance of <see cref="MainWindow"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="navigationService">Navigation service.</param>
        public MainWindow(
            ILogger<MainWindow> logger,
            IWavesNavigationService navigationService)
            : base(logger, navigationService)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var os = AvaloniaLocator
                .Current
                .GetService<IRuntimePlatform>() !
                .GetRuntimeInfo()
                .OperatingSystem;

            switch (os)
            {
                case OperatingSystemType.WinNT:
                    Padding = new Thickness(0, 40, 0, 0);
                    break;
                case OperatingSystemType.Linux:
                    break;
                case OperatingSystemType.OSX:
                    Padding = new Thickness(0, 28, 0, 0);
                    break;
            }
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
