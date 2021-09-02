using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Waves.Core.Base.Attributes;
using Waves.Core.Base.Interfaces;
using Waves.Core.Plugins.Services.EventArgs;
using Waves.UI.Avalonia.Animations.Extensions;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Avalonia.Plugins.Services.Interfaces;
using Waves.UI.Plugins.Services;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Interfaces;

namespace Waves.UI.Avalonia.Plugins.Services
{
    /// <summary>
    /// Navigation service.
    /// </summary>
    [WavesService(typeof(IWavesNavigationService))]
    public class WavesNavigationService : WavesNavigationServiceBase
    {
        private readonly IWavesWindowStyleService _windowStyleService;
        
        /// <summary>
        /// Creates new instance of <see cref="WavesNavigationService"/>.
        /// </summary>
        /// <param name="core">Instance of core.</param>
        /// <param name="windowStyleService">Instance of <see cref="IWavesWindowStyleService"/>.</param>
        public WavesNavigationService(
            IWavesCore core,
            IWavesWindowStyleService windowStyleService)
            :base(core)
        {
            _windowStyleService = windowStyleService;
        }
        
        /// <summary>
        /// Gets dictionary of Content controls keyed by region.
        /// </summary>
        private Dictionary<string, ContentControl> ContentControls { get; set; }

        /// <inheritdoc />
        public override Task InitializeAsync()
        {
            ContentControls = new Dictionary<string, ContentControl>();
            return base.InitializeAsync();
        }

        /// <inheritdoc />
        public override void RegisterContentControl(string region, object contentControl)
        {
            if (contentControl is not ContentControl control)
            {
                return;
            }

            AddContentControl(region, control);

            if (!PendingActions.ContainsKey(region))
            {
                return;
            }

            PendingActions[region].Invoke();
            PendingActions.Remove(region);
        }

        /// <inheritdoc />
        public override void UnregisterContentControl(string region)
        {
            if (!ContentControls.ContainsKey(region))
            {
                return;
            }

            ContentControls.Remove(region);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "AvaloniaUI Navigation Service";
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            base.Dispose();
            
            if (!disposing)
            {
                return;
            }

            ContentControls.Clear();
        }

        /// <summary>
        /// Navigates to windows.
        /// </summary>
        /// <param name="view">Window view.</param>
        /// <param name="viewModel">ViewModel.</param>
        protected override async Task InitializeWindowAsync(IWavesWindow view, IWavesViewModel viewModel)
        {
            var region = await InitializeComponents(view, viewModel);
            var contentControl = view as ContentControl;
            if (contentControl == null)
            {
                return;
            }
            
            void Action()
            {
                _windowStyleService.UpdateWindowStyle(view);
                view.Show();
                RegisterView(contentControl);
            }
            
            await Dispatcher.UIThread.InvokeAsync(Action);

            AddContentControl(region, contentControl);
        }

        /// <summary>
        /// Navigates to page.
        /// </summary>
        /// <param name="view">Page view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        protected override async Task InitializePageAsync(IWavesPage view, IWavesViewModel viewModel, bool addToHistory = true)
        {
            var region = await InitializeComponents(view, viewModel);

            void Action()
            {
                AddToHistoryStack(region, viewModel, addToHistory);
                var contentControl = ContentControls[region];
                if (contentControl is WavesWindow window)
                {
                    window.FrontLayerContent = null;
                }

                if (contentControl.Content != null && contentControl.Content.GetType() == view.GetType())
                {
                    return;
                }

                FadeOutUiElement(contentControl);
                UnregisterView(contentControl);
                ContentControls[region].Content = view;
                FadeInUiElement(contentControl);
                RegisterView(contentControl);

                OnGoBackChanged(
                    new GoBackNavigationEventArgs(
                        Histories[region].Count > 1,
                        ContentControls[region]));
            }

            if (!ContentControls.ContainsKey(region))
            {
                PendingActions.Add(region, Action);
            }
            else
            {
                await Dispatcher.UIThread.InvokeAsync(Action);
            }
        }

        /// <summary>
        /// Navigates to user control.
        /// </summary>
        /// <param name="view">User control view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        protected override async Task InitializeUserControlAsync(IWavesUserControl view, IWavesViewModel viewModel, bool addToHistory = true)
        {
            var region = await InitializeComponents(view, viewModel);
        
            void Action()
            {
                AddToHistoryStack(region, viewModel, addToHistory);
                var contentControl = ContentControls[region];
                FadeOutUiElement(contentControl);
                UnregisterView(contentControl);
                view.Opacity = 0;
                ContentControls[region].Content = view;
                FadeInUiElement(contentControl);
                RegisterView(contentControl);
            }
        
            if (!ContentControls.ContainsKey(region))
            {
                PendingActions.Add(region, Action);
            }
            else
            {
                await Dispatcher.UIThread.InvokeAsync(Action);
            }
        }
        
        /// <summary>
        /// Navigates to dialog.
        /// </summary>
        /// <param name="view">Dialog view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        protected override async Task InitializeDialogAsync(IWavesDialog view, IWavesDialogViewModel viewModel, bool addToHistory = true)
        {
            var region = await InitializeComponents(view, viewModel);
            var styledElement = view as StyledElement;
            if (styledElement == null)
            {
                return;
            }
            
            void Action()
            {
                AddToHistoryStack(region, viewModel, addToHistory);
                DialogSessions.Add(viewModel);
                CheckDialogs();
                var contentControl = ContentControls[region];
                if (contentControl is WavesWindow window)
                {
                    UnregisterView(contentControl);
                    window.FrontLayerContent = styledElement;
                    RegisterView(contentControl);
                }
                else
                {
                    // TODO: what if another content control?
                }
            }
        
            if (!ContentControls.ContainsKey(region))
            {
                PendingActions.Add(region, Action);
            }
            else
            {
                await Dispatcher.UIThread.InvokeAsync(Action);
            }
        }

        /// <summary>
        /// Checks dialogs.
        /// </summary>
        private void CheckDialogs()
        {
            if (DialogSessions.Count > 0)
            {
                OnDialogsShown();
            }
            else
            {
                OnDialogsHidden();
            }
        }

        /// <summary>
        /// Adds new window to content control dictionary.
        /// </summary>
        /// <param name="region">Region.</param>
        /// <param name="view">Content control.</param>
        private void AddContentControl(string region, ContentControl view)
        {
            if (!ContentControls.ContainsKey(region))
            {
                ContentControls.Add(region, view);
            }
            else
            {
                // rewrite if controls with same region are not equal.
                if (ContentControls[region].Equals(view))
                {
                    return;
                }

                ContentControls[region] = view;
            }
        }

        /// <summary>
        /// Animates fade in for <see cref="StyledElement"/> is current <see cref="ContentControl"/>.
        /// </summary>
        /// <param name="control">Instance of <see cref="ContentControl"/>.</param>
        private void FadeInUiElement(ContentControl control)
        {
            if (control.Content is not StyledElement element)
            {
                return;
            }

            element.AnimateOpacity(0, 1, 100);
        }

        /// <summary>
        /// Animates fade out for <see cref="StyledElement"/> is current <see cref="ContentControl"/>.
        /// </summary>
        /// <param name="control">Instance of <see cref="ContentControl"/>.</param>
        private void FadeOutUiElement(ContentControl control)
        {
            if (control.Content is not StyledElement element)
            {
                return;
            }

            element.AnimateOpacity(1, 0, 100);
        }

        /// <summary>
        /// Invokes <see cref="IWavesViewModel.ViewAppeared"/> for <see cref="StyledElement"/> is current <see cref="ContentControl"/>.
        /// </summary>
        /// <param name="control">Instance of <see cref="ContentControl"/>.</param>
        private void RegisterView(ContentControl control)
        {
            if (control.Content is not StyledElement element)
            {
                return;
            }

            // TODO: regions?
            // var controls = element.FindRegions(this);

            if (element.DataContext is IWavesViewModel viewModel)
            {
                viewModel.ViewAppeared();
            }
        }

        /// <summary>
        /// Invokes <see cref="IWavesViewModel.ViewDisappeared"/> for <see cref="StyledElement"/> is current <see cref="ContentControl"/>.
        /// </summary>
        /// <param name="control">Instance of <see cref="ContentControl"/>.</param>
        private void UnregisterView(ContentControl control)
        {
            if (control.Content is not StyledElement element)
            {
                return;
            }

            if (element.DataContext is IWavesViewModel viewModel)
            {
                viewModel.ViewDisappeared();
            }

            if (element is IWavesView view)
            {
                view.Dispose();
            }
        }
    }
}
