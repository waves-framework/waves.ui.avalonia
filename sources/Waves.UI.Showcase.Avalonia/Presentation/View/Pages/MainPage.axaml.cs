using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;
using Waves.UI.Services.Interfaces;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Pages;

namespace Waves.UI.Showcase.Avalonia.Presentation.View.Pages
{
    /// <summary>
    /// Main page.
    /// </summary>
    [WavesView(typeof(MainPageViewModel))]
    public partial class MainPage : WavesPage
    {
        /// <summary>
        /// Creates new instance os <see cref="MainPage"/>.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates new instance of <see cref="MainPage"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="navigationService">Navigation service.</param>
        public MainPage(
            ILogger<MainPage> logger,
            IWavesNavigationService navigationService)
            : base(logger, navigationService)
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
