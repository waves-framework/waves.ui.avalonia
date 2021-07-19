using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Waves.Core.Base;
using Waves.Core.Base.Attributes;
using Waves.Core.Base.Interfaces;
using Waves.Core.Extensions;
using Waves.Core.Plugins.Services.EventArgs;
using Waves.UI.Avalonia.Animations.Extensions;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Avalonia.Plugins.Services.Interfaces;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Extensions;
using Waves.UI.Presentation.Interfaces;

namespace Waves.UI.Avalonia.Plugins.Services
{
    /// <summary>
    /// Navigation service.
    /// </summary>
    [WavesService(typeof(IWavesNavigationService))]
    public class WavesNavigationService : WavesService, IWavesNavigationService
    {
        private readonly IWavesWindowStyleService _windowStyleService;

        private List<IWavesDialogViewModel> _dialogSessions;
        private Dictionary<string, Action> _pendingActions;

        /// <summary>
        /// Creates new instance of <see cref="WavesNavigationService"/>.
        /// </summary>
        /// <param name="core">Instance of core.</param>
        /// <param name="windowStyleService">Instance of <see cref="IWavesWindowStyleService"/>.</param>
        public WavesNavigationService(IWavesCore core, IWavesWindowStyleService windowStyleService)
        {
            Core = core;

            _windowStyleService = windowStyleService;
        }

        /// <inheritdoc />
        public event EventHandler<GoBackNavigationEventArgs> GoBackChanged;

        /// <inheritdoc />
        public event EventHandler DialogsShown;

        /// <inheritdoc />
        public event EventHandler DialogsHidden;

        /// <summary>
        /// Gets core.
        /// </summary>
        public IWavesCore Core { get; }

        /// <summary>
        /// Gets dictionary of Content controls keyed by region.
        /// </summary>
        private Dictionary<string, ContentControl> ContentControls { get; set; }

        /// <summary>
        /// Gets dictionary of view models keyed by region.
        /// </summary>
        private Dictionary<string, Stack<IWavesViewModel>> Histories { get; set; }

        /// <inheritdoc />
        public override Task InitializeAsync()
        {
            ContentControls = new Dictionary<string, ContentControl>();
            Histories = new Dictionary<string, Stack<IWavesViewModel>>();

            _dialogSessions = new List<IWavesDialogViewModel>();
            _pendingActions = new Dictionary<string, Action>();

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task GoBackAsync(IWavesViewModel viewModel)
        {
            foreach (var pair in Histories)
            {
                var history = pair.Value;

                if (!Enumerable.Contains(history, viewModel))
                {
                    continue;
                }

                if (history.Count <= 1)
                {
                    return;
                }

                var removingViewModel = history.Pop();
                if (removingViewModel is IWavesDialogViewModel removingDialogViewModel)
                {
                    _dialogSessions.Remove(removingDialogViewModel);
                }

                CheckDialogs();

                await NavigateAsync(history.First(), false);

                return;
            }
        }

        /// <inheritdoc />
        public async Task GoBackAsync(
            string region)
        {
            var history = Histories[region];

            if (history.Count <= 1)
            {
                return;
            }

            var removingViewModel = history.Pop();
            if (removingViewModel is IWavesDialogViewModel removingDialogViewModel)
            {
                _dialogSessions.Remove(removingDialogViewModel);
            }

            CheckDialogs();

            await NavigateAsync(history.First(), false);
        }

        /// <inheritdoc />
        public async Task NavigateAsync<T>(bool addToHistory = true)
            where T : class
        {
            var viewModel = await Core.GetInstanceAsync<T>();
            await NavigateAsync((IWavesViewModel)viewModel, addToHistory);
        }

        /// <inheritdoc />
        public async Task NavigateAsync<T, TParameter>(
            TParameter parameter,
            bool addToHistory = true)
            where T : class
        {
            var viewModel = await Core.GetInstanceAsync<T>();
            await NavigateAsync((IWavesParameterizedViewModel<TParameter>)viewModel, parameter, addToHistory);
        }

        /// <inheritdoc />
        public async Task<TResult> NavigateAsync<T, TResult>(
            bool addToHistory = true)
            where T : class
        {
            var viewModel = await Core.GetInstanceAsync<T>();
            return await NavigateAsync((IWavesViewModel<TResult>)viewModel, addToHistory);
        }

        /// <inheritdoc />
        public async Task<TResult> NavigateAsync<T, TParameter, TResult>(
            TParameter parameter,
            bool addToHistory = true)
            where T : class
        {
            var viewModel = await Core.GetInstanceAsync<T>();
            return await NavigateAsync((IWavesViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
        }

        /// <inheritdoc />
        public void RegisterContentControl(string region, object contentControl)
        {
            if (contentControl is not ContentControl control)
            {
                return;
            }

            AddContentControl(region, control);

            if (!_pendingActions.ContainsKey(region))
            {
                return;
            }

            _pendingActions[region].Invoke();
            _pendingActions.Remove(region);
        }

        /// <inheritdoc />
        public void UnregisterContentControl(string region)
        {
            if (!ContentControls.ContainsKey(region))
            {
                return;
            }

            ContentControls.Remove(region);
        }

        /// <inheritdoc />
        public async Task NavigateAsync(IWavesViewModel viewModel, bool addToHistory = true)
        {
            try
            {
                var view = await Core.GetInstanceAsync<IWavesView>(viewModel.GetType());

                switch (view)
                {
                    case WavesWindow window:
                        await InitializeWindowAsync(window, viewModel);
                        break;
                    // case WavesUserControl userControl:
                    //     await InitializeUserControlAsync(userControl, viewModel, addToHistory);
                    //     break;
                    // case WavesPage page:
                    //     await InitializePageAsync(page, viewModel, addToHistory);
                    //     break;
                    // case WavesDialog dialog:
                    //     await InitializeDialogAsync(dialog, (IWavesDialogViewModel)viewModel, addToHistory);
                    //     break;
                }
            }
            catch (Exception e)
            {
                await Core.WriteLogAsync(e, this);
            }
        }

        /// <inheritdoc />
        public async Task NavigateAsync<TParameter>(
            IWavesParameterizedViewModel<TParameter> viewModel,
            TParameter parameter,
            bool addToHistory = true)
        {
            try
            {
                var view = await Core.GetInstanceAsync<IWavesView>(viewModel.GetType());

                switch (view)
                {
                    case WavesWindow window:
                        await NavigateToWindowAsync(window, viewModel, parameter);
                        break;
                    // case WavesUserControl userControl:
                    //     await NavigateToUserControlAsync(userControl, viewModel, parameter, addToHistory);
                    //     break;
                    // case WavesPage page:
                    //     await NavigateToPageAsync(page, viewModel, parameter, addToHistory);
                    //     break;
                    // case WavesDialog dialog:
                    //     await NavigateToDialogAsync(dialog, viewModel, parameter, addToHistory);
                    //     break;
                }
            }
            catch (Exception e)
            {
                await Core.WriteLogAsync(e, this);
            }
        }

        /// <inheritdoc />
        public async Task<TResult> NavigateAsync<TResult>(
            IWavesViewModel<TResult> viewModel,
            bool addToHistory = true)
        {
            try
            {
                var view = await Core.GetInstanceAsync<IWavesView>(viewModel.GetType());

                switch (view)
                {
                    case WavesWindow window:
                        return await NavigateToWindowAsync(window, viewModel);
                    // case WavesUserControl userControl:
                    //     return await NavigateToUserControlAsync(userControl, viewModel, addToHistory);
                    // case WavesPage page:
                    //     return await NavigateToPageAsync(page, viewModel, addToHistory);
                    // case WavesDialog dialog:
                    //     return await NavigateToDialogAsync(dialog, (IWavesDialogViewModel<TResult>)viewModel, addToHistory);
                }
            }
            catch (Exception e)
            {
                await Core.WriteLogAsync(e, this);
            }

            return default;
        }

        /// <inheritdoc />
        public async Task<TResult> NavigateAsync<TParameter, TResult>(
            IWavesViewModel<TParameter, TResult> viewModel,
            TParameter parameter,
            bool addToHistory = true)
        {
            try
            {
                var view = await Core.GetInstanceAsync<IWavesView>(viewModel.GetType());

                switch (view)
                {
                    case WavesWindow window:
                        return await NavigateToWindowAsync(window, viewModel, parameter);
                    // case WavesUserControl userControl:
                    //     return await NavigateToUserControlAsync(userControl, viewModel, parameter, addToHistory);
                    // case WavesPage page:
                    //     return await NavigateToPageAsync(page, viewModel, parameter, addToHistory);
                    // case WavesDialog dialog:
                    //     return await NavigateToDialogAsync(dialog, (IWavesDialogViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
                }
            }
            catch (Exception e)
            {
                await Core.WriteLogAsync(e, this);
            }

            return default;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "AvaloniaUI Navigation Service";
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            ContentControls.Clear();
            Histories.Clear();

            // TODO: your code for release unmanaged resources.
        }

        /// <summary>
        /// Callback that notifies that go back changed.
        /// </summary>
        /// <param name="e">Arguments.</param>
        protected virtual void OnGoBackChanged(
            GoBackNavigationEventArgs e)
        {
            GoBackChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Notifies when dialogs shown.
        /// </summary>
        protected virtual void OnDialogsShown()
        {
            DialogsShown?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Notifies when all dialogs hidden.
        /// </summary>
        protected virtual void OnDialogsHidden()
        {
            DialogsHidden?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Navigates to windows.
        /// </summary>
        /// <param name="view">Window view.</param>
        /// <param name="viewModel">ViewModel.</param>
        private async Task InitializeWindowAsync(WavesWindow view, IWavesViewModel viewModel)
        {
            var region = await InitializeComponents(view, viewModel);

            void Action()
            {
                _windowStyleService.UpdateWindowStyle(view);
                view.Show();
                RegisterView(view);
            }
            
            await Dispatcher.UIThread.InvokeAsync(Action);

            AddContentControl(region, view);
        }

        // /// <summary>
        // /// Navigates to page.
        // /// </summary>
        // /// <param name="view">Page view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task InitializePageAsync(WavesPage view, IWavesViewModel viewModel, bool addToHistory = true)
        // {
        //     var region = await InitializeComponents(view, viewModel);
        //
        //     void Action()
        //     {
        //         AddToHistoryStack(region, viewModel, addToHistory);
        //         var contentControl = ContentControls[region];
        //         if (contentControl is WavesWindow window)
        //         {
        //             window.FrontLayerContent = null;
        //         }
        //
        //         if (contentControl.Content != null && contentControl.Content.GetType() == view.GetType())
        //         {
        //             return;
        //         }
        //
        //         FadeOutUiElement(contentControl);
        //         UnregisterView(contentControl);
        //         view.Opacity = 0;
        //         ContentControls[region].Content = view;
        //         FadeInUiElement(contentControl);
        //         RegisterView(contentControl);
        //
        //         OnGoBackChanged(
        //             new GoBackNavigationEventArgs(
        //                 Histories[region].Count > 1,
        //                 ContentControls[region]));
        //     }
        //
        //     if (!ContentControls.ContainsKey(region))
        //     {
        //         _pendingActions.Add(region, Action);
        //     }
        //     else
        //     {
        //         DispatcherHelper.Invoke(Action);
        //     }
        // }
        //
        // /// <summary>
        // /// Navigates to user control.
        // /// </summary>
        // /// <param name="view">User control view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task InitializeUserControlAsync(WavesUserControl view, IWavesViewModel viewModel, bool addToHistory = true)
        // {
        //     var region = await InitializeComponents(view, viewModel);
        //
        //     void Action()
        //     {
        //         AddToHistoryStack(region, viewModel, addToHistory);
        //         var contentControl = ContentControls[region];
        //         FadeOutUiElement(contentControl);
        //         UnregisterView(contentControl);
        //         view.Opacity = 0;
        //         ContentControls[region].Content = view;
        //         FadeInUiElement(contentControl);
        //         RegisterView(contentControl);
        //     }
        //
        //     if (!ContentControls.ContainsKey(region))
        //     {
        //         _pendingActions.Add(region, Action);
        //     }
        //     else
        //     {
        //         DispatcherHelper.Invoke(Action);
        //     }
        // }
        //
        // /// <summary>
        // /// Navigates to dialog.
        // /// </summary>
        // /// <param name="view">Dialog view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task InitializeDialogAsync(WavesDialog view, IWavesDialogViewModel viewModel, bool addToHistory = true)
        // {
        //     var region = await InitializeComponents(view, viewModel);
        //
        //     void Action()
        //     {
        //         AddToHistoryStack(region, viewModel, addToHistory);
        //         _dialogSessions.Add(viewModel);
        //         CheckDialogs();
        //         var contentControl = ContentControls[region];
        //         if (contentControl is WavesWindow window)
        //         {
        //             UnregisterView(contentControl);
        //             window.FrontLayerContent = view;
        //             RegisterView(contentControl);
        //         }
        //         else
        //         {
        //             // TODO: what if another content control?
        //         }
        //     }
        //
        //     if (!ContentControls.ContainsKey(region))
        //     {
        //         _pendingActions.Add(region, Action);
        //     }
        //     else
        //     {
        //         DispatcherHelper.Invoke(Action);
        //     }
        // }

        /// <summary>
        /// Navigates to windows.
        /// </summary>
        /// <param name="view">Window view.</param>
        /// <param name="viewModel">ViewModel.</param>
        private async Task<TResult> NavigateToWindowAsync<TResult>(WavesWindow view, IWavesViewModel<TResult> viewModel)
        {
            await InitializeWindowAsync(view, viewModel);
            return viewModel.Result;
        }

        /// <summary>
        /// Navigates to window.
        /// </summary>
        /// <param name="view">Window view.</param>
        /// <param name="viewModel">ViewModel.</param>
        /// <param name="parameter">Parameter.</param>
        private async Task NavigateToWindowAsync<TParameter>(WavesWindow view, IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter)
        {
            await viewModel.Prepare(parameter);
            await InitializeWindowAsync(view, viewModel);
        }

        /// <summary>
        /// Navigates to window.
        /// </summary>
        /// <param name="view">Window view.</param>
        /// <param name="viewModel">ViewModel.</param>
        /// <param name="parameter">Parameter.</param>
        private async Task<TResult> NavigateToWindowAsync<TParameter, TResult>(WavesWindow view, IWavesViewModel<TParameter, TResult> viewModel, TParameter parameter)
        {
            await viewModel.Prepare(parameter);
            await InitializeWindowAsync(view, viewModel);
            return viewModel.Result;
        }

        // /// <summary>
        // /// Navigates to page.
        // /// </summary>
        // /// <param name="view">Page view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task<TResult> NavigateToPageAsync<TResult>(WavesPage view, IWavesViewModel<TResult> viewModel, bool addToHistory = true)
        // {
        //     await InitializePageAsync(view, viewModel, addToHistory);
        //     return viewModel.Result;
        // }
        //
        // /// <summary>
        // /// Navigates to page.
        // /// </summary>
        // /// <param name="view">Page view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="parameter">Parameter.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task NavigateToPageAsync<TParameter>(WavesPage view, IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter, bool addToHistory = true)
        // {
        //     await viewModel.Prepare(parameter);
        //     await InitializePageAsync(view, viewModel, addToHistory);
        // }
        //
        // /// <summary>
        // /// Navigates to page.
        // /// </summary>
        // /// <param name="view">Page view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="parameter">Parameter.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task<TResult> NavigateToPageAsync<TParameter, TResult>(WavesPage view, IWavesViewModel<TParameter, TResult> viewModel, TParameter parameter, bool addToHistory = true)
        // {
        //     await viewModel.Prepare(parameter);
        //     await InitializePageAsync(view, viewModel, addToHistory);
        //     return viewModel.Result;
        // }

        // /// <summary>
        // /// Navigates to user control.
        // /// </summary>
        // /// <param name="view">User control view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task<TResult> NavigateToUserControlAsync<TResult>(WavesUserControl view, IWavesViewModel<TResult> viewModel, bool addToHistory = true)
        // {
        //     await InitializeUserControlAsync(view, viewModel, addToHistory);
        //     return viewModel.Result;
        // }
        //
        // /// <summary>
        // /// Navigates to user control.
        // /// </summary>
        // /// <param name="view">User control view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="parameter">Parameter.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task NavigateToUserControlAsync<TParameter>(WavesUserControl view, IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter, bool addToHistory = true)
        // {
        //     await viewModel.Prepare(parameter);
        //     await InitializeUserControlAsync(view, viewModel, addToHistory);
        // }
        //
        // /// <summary>
        // /// Navigates to user control.
        // /// </summary>
        // /// <param name="view">User control view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="parameter">Parameter.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task<TResult> NavigateToUserControlAsync<TParameter, TResult>(WavesUserControl view, IWavesViewModel<TParameter, TResult> viewModel, TParameter parameter, bool addToHistory = true)
        // {
        //     await viewModel.Prepare(parameter);
        //     await InitializeUserControlAsync(view, viewModel, addToHistory);
        //     return viewModel.Result;
        // }
        //
        // /// <summary>
        // /// Navigates to dialog.
        // /// </summary>
        // /// <param name="view">Dialog view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task<TResult> NavigateToDialogAsync<TResult>(WavesDialog view, IWavesDialogViewModel<TResult> viewModel, bool addToHistory = true)
        // {
        //     var completionSource = new TaskCompletionSource<TResult>();
        //     await InitializeDialogAsync(view, viewModel, addToHistory).LogExceptions(Core);
        //     InitializeDialog(viewModel, completionSource);
        //     return await completionSource.Task;
        // }
        //
        // /// <summary>
        // /// Navigates to dialog.
        // /// </summary>
        // /// <param name="view">Dialog view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="parameter">Parameter.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task NavigateToDialogAsync<TParameter>(WavesDialog view, IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter, bool addToHistory = true)
        // {
        //     await viewModel.Prepare(parameter);
        //     await InitializeDialogAsync(view, (IWavesDialogViewModel)viewModel, addToHistory);
        // }
        //
        // /// <summary>
        // /// Navigates to dialog.
        // /// </summary>
        // /// <param name="view">Dialog view.</param>
        // /// <param name="viewModel">View model.</param>
        // /// <param name="parameter">Parameter.</param>
        // /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        // private async Task<TResult> NavigateToDialogAsync<TParameter, TResult>(WavesDialog view, IWavesDialogViewModel<TParameter, TResult> viewModel, TParameter parameter, bool addToHistory = true)
        // {
        //     await viewModel.Prepare(parameter);
        //     var completionSource = new TaskCompletionSource<TResult>();
        //     await InitializeDialogAsync(view, viewModel, addToHistory).LogExceptions(Core);
        //     InitializeDialog(viewModel, completionSource);
        //     return await completionSource.Task;
        // }

        /// <summary>
        /// Initializes dialog with result.
        /// </summary>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <param name="viewModel">View model.</param>
        /// <param name="completionSource">Completion source.</param>
        private void InitializeDialog<TResult>(IWavesDialogViewModel<TResult> viewModel, TaskCompletionSource<TResult> completionSource)
        {
            void OnDone(object sender, EventArgs e)
            {
                Unsubscribe();
                completionSource.SetResult(viewModel.Result);
            }

            void OnCancel(object sender, EventArgs e)
            {
                Unsubscribe();
                completionSource.SetResult(viewModel.Result);
            }

            void Unsubscribe()
            {
                viewModel.Done -= OnDone;
                viewModel.Cancel -= OnCancel;
            }

            viewModel.Done += OnDone;
            viewModel.Cancel += OnCancel;
        }

        /// <summary>
        /// Checks dialogs.
        /// </summary>
        private void CheckDialogs()
        {
            if (_dialogSessions.Count > 0)
            {
                OnDialogsShown();
            }
            else
            {
                OnDialogsHidden();
            }
        }

        /// <summary>
        /// Adds viewModel to history stack or just create new history stack by region.
        /// </summary>
        /// <param name="region">Region.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        private void AddToHistoryStack(string region, IWavesViewModel viewModel, bool addToHistory = true)
        {
            if (!Histories.ContainsKey(region))
            {
                Histories.Add(region, new Stack<IWavesViewModel>());
            }

            if (addToHistory)
            {
                Histories[region].Push(viewModel);
            }
        }

        /// <summary>
        /// Initializes View and ViewModel and return it's region.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="viewModel">View model.</param>
        /// <returns>Returns region.</returns>
        private async Task<string> InitializeComponents(IWavesView view, IWavesViewModel viewModel)
        {
            var attribute = view.GetViewAttribute();
            var region = attribute.Region;

            if (!viewModel.IsInitialized)
            {
                await viewModel.InitializeAsync();
            }

            //// We don't need to initialize view
            //// because automatically by WPF framework.

            view.DataContext = viewModel;
            return region;
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
        /// Animates fade in for <see cref="UIElement"/> is current <see cref="ContentControl"/>.
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
        /// Animates fade out for <see cref="UIElement"/> is current <see cref="ContentControl"/>.
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
        /// Invokes <see cref="IWavesViewModel.ViewAppeared"/> for <see cref="UIElement"/> is current <see cref="ContentControl"/>.
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
        /// Invokes <see cref="IWavesViewModel.ViewDisappeared"/> for <see cref="UIElement"/> is current <see cref="ContentControl"/>.
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
