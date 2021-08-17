using Avalonia.Markup.Xaml;
using Waves.Core.Base.Interfaces;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Attributes;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Pages;

namespace Waves.UI.Showcase.Avalonia.Presentation.View.Pages
{
    /// <summary>
    /// Main page view.
    /// </summary>
    [WavesView(typeof(MainPageViewModel))]
    public partial class MainPageView : WavesPage
    {
        /// <inheritdoc />
        public MainPageView() : base()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        public MainPageView(
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
