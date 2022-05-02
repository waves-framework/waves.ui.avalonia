using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;
using Waves.UI.Services.Interfaces;
using Waves.UI.Showcase.Common.Presentation.ViewModel.UserControls;

namespace Waves.UI.Showcase.Avalonia.Presentation.View.UserControls
{
    /// <summary>
    /// Main window.
    /// </summary>
    [WavesView(typeof(TestUserControlViewModel), "Test")]
    public partial class TestUserControl : WavesUserControl
    {
        /// <summary>
        /// Creates new instance os <see cref="TestUserControl"/>.
        /// </summary>
        public TestUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates new instance of <see cref="TestUserControl"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="navigationService">Navigation service.</param>
        public TestUserControl(
            ILogger<TestUserControl> logger,
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
