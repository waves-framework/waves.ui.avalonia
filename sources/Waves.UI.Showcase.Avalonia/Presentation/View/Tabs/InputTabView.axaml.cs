using Avalonia.Markup.Xaml;
using Waves.Core.Base.Interfaces;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Attributes;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Pages;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Tabs;

namespace Waves.UI.Showcase.Avalonia.Presentation.View.Tabs
{
    /// <summary>
    /// Input tab view.
    /// </summary>
    [WavesView(typeof(InputTabViewModel))]
    public partial class InputTabView : WavesPage
    {
        /// <inheritdoc />
        public InputTabView() : base()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        public InputTabView(
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
