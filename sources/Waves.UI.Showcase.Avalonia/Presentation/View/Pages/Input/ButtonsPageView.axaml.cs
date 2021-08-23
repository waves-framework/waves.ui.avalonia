using Avalonia.Markup.Xaml;
using Waves.Core.Base.Interfaces;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Attributes;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Pages.Input;

namespace Waves.UI.Showcase.Avalonia.Presentation.View.Pages.Input
{
    /// <summary>
    /// Input tab view.
    /// </summary>
    [WavesView(typeof(ButtonsPageViewModel))]
    public partial class ButtonsPageView : WavesUserControl
    {
        /// <inheritdoc />
        public ButtonsPageView() : base()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        public ButtonsPageView(
            IWavesCore core,
            IWavesNavigationService navigationService)
            : base(core, navigationService)
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
