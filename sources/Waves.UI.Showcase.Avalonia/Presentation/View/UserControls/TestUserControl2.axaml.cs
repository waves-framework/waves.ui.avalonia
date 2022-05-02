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
    [WavesView(typeof(TestUserControlViewModel2), "Test")]
    public partial class TestUserControl2 : WavesUserControl
    {
        /// <summary>
        /// Creates new instance os <see cref="TestUserControl2"/>.
        /// </summary>
        public TestUserControl2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates new instance of <see cref="TestUserControl2"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="navigationService">Navigation service.</param>
        public TestUserControl2(
            ILogger<TestUserControl2> logger,
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
