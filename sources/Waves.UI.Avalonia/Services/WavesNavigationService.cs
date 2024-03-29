#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Waves.Core;
using Waves.Core.Base.Attributes;
using Waves.Core.Base.Enums;
using Waves.Core.Extensions;
using Waves.Core.Services.Interfaces;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.EventArgs;
using Waves.UI.Dialogs;
using Waves.UI.Dialogs.Enums;
using Waves.UI.Dialogs.Interfaces;
using Waves.UI.Presentation.Interfaces.View;
using Waves.UI.Presentation.Interfaces.View.Controls;
using Waves.UI.Presentation.Interfaces.ViewModel;
using Waves.UI.Services;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Services;

/// <summary>
/// Waves Avalonia navigation service.
/// </summary>
[WavesPlugin(typeof(IWavesNavigationService), WavesLifetime.Singleton)]
public class WavesNavigationService :
    WavesNavigationServiceBase<object>
{
    private readonly Dictionary<string, ContentControl> _contentControls;
    private readonly List<IWavesDialogViewModel> _dialogSessions;

    private Window? _mainWindow;
    private Control? _mainPage;

    /// <summary>
    /// Creates new instance of <see cref="WavesNavigationService"/>.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    /// <param name="configuration">Configuration.</param>
    /// <param name="logger">Logger.</param>
    public WavesNavigationService(
        IWavesServiceProvider serviceProvider,
        IConfiguration configuration,
        ILogger<WavesNavigationService> logger)
        : base(serviceProvider, configuration, logger)
    {
        _contentControls = new Dictionary<string, ContentControl>();
        _dialogSessions = new List<IWavesDialogViewModel>();
    }

    /// <inheritdoc />
    public override async Task<WavesOpenFileDialogResult> ShowOpenFileDialogAsync(IEnumerable<WavesFileDialogFilter> filters)
    {
        var keyPair = OpenedWindows.FirstOrDefault();
        if (keyPair.Value is WavesWindow window)
        {
            var dialog = new OpenFileDialog();

            foreach (var filter in filters)
            {
                dialog.Filters.Add(new FileDialogFilter
                {
                    Name = filter.Name,
                    Extensions = new List<string>(filter.Extensions),
                });
            }

            var result = await dialog.ShowAsync(window);

            return result.Length > 0
                ? new WavesOpenFileDialogResult
                {
                    Result = WavesDialogResult.Ok,
                    FileNames = new List<string>(result),
                }
                : new WavesOpenFileDialogResult
                {
                    Result = WavesDialogResult.Cancel,
                    FileNames = new List<string>(result),
                };
        }

        return null;
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
        if (!_contentControls.ContainsKey(region))
        {
            return;
        }

        _contentControls.Remove(region);
    }

    /// <inheritdoc />
    protected override async Task InitializeWindowAsync(IWavesWindow<object> view, IWavesViewModel viewModel)
    {
        // first invoke of this method
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && _mainWindow == null)
        {
            _mainWindow = view as Window;

            if (_mainWindow != null)
            {
                desktop.MainWindow = _mainWindow;
            }
        }

        var region = await InitializeComponents(view, viewModel);
        var contentControl = view as ContentControl;
        if (contentControl == null)
        {
            return;
        }

        void Action()
        {
            view.Show();
            OpenedWindows.Add(viewModel, view);
            RegisterView(contentControl);
            Logger.LogDebug("Navigation to view {ViewType} with data context {ViewModelType} in region {Region} completed", view.GetType(), viewModel.GetType(), region);
            viewModel.RunPostInitializationAsync().FireAndForget();
        }

        await Dispatcher.UIThread.InvokeAsync(Action);

        AddContentControl(region, contentControl);
    }

    /// <inheritdoc />
    protected override async Task InitializePageAsync(IWavesPage<object> view, IWavesViewModel viewModel, bool addToHistory = true)
    {
        // first invoke of this method
        if (Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform && _mainPage == null)
        {
            _mainPage = view as UserControl;

            if (_mainPage != null)
            {
                singleViewPlatform.MainView = _mainPage;
            }
        }

        var region = await InitializeComponents(view, viewModel);

        void Action()
        {
            AddToHistoryStack(region, viewModel, addToHistory);
            var contentControl = _contentControls[region];
            if (contentControl is WavesWindow window)
            {
                window.FrontContent = null;
            }

            if (contentControl.Content != null && contentControl.Content.GetType() == view.GetType())
            {
                return;
            }

            UnregisterView(contentControl.Content);
            _contentControls[region].Content = view;
            RegisterView(contentControl.Content);

            OnGoBackChanged(
                new GoBackNavigationEventArgs(
                    Histories[region].Count > 1,
                    _contentControls[region]));

            Logger.LogDebug("Navigation to view {ViewType} with data context {ViewModelType} in region {Region} completed", view.GetType(), viewModel.GetType(), region);
            viewModel.RunPostInitializationAsync().FireAndForget();
        }

        if (!_contentControls.ContainsKey(region))
        {
            PendingActions.Add(region, Action);
        }
        else
        {
            await Dispatcher.UIThread.InvokeAsync(Action);
        }
    }

    /// <inheritdoc />
    protected override async Task InitializeUserControlAsync(IWavesUserControl<object> view, IWavesViewModel viewModel, bool addToHistory = true)
    {
        var region = await InitializeComponents(view, viewModel);

        void Action()
        {
            AddToHistoryStack(region, viewModel, addToHistory);
            var contentControl = _contentControls[region];
            if (contentControl is WavesWindow window)
            {
                window.FrontContent = null;
            }

            if (contentControl.Content != null && contentControl.Content.GetType() == view.GetType())
            {
                return;
            }

            UnregisterView(contentControl.Content);
            _contentControls[region].Content = view;
            RegisterView(contentControl.Content);

            OnGoBackChanged(
                new GoBackNavigationEventArgs(
                    Histories[region].Count > 1,
                    _contentControls[region]));

            Logger.LogDebug("Navigation to view {ViewType} with data context {ViewModelType} in region {Region} completed", view.GetType(), viewModel.GetType(), region);
            viewModel.RunPostInitializationAsync().FireAndForget();
        }

        if (!_contentControls.ContainsKey(region))
        {
            PendingActions.Add(region, Action);
        }
        else
        {
            await Dispatcher.UIThread.InvokeAsync(Action);
        }
    }

    /// <inheritdoc />
    protected override async Task InitializeDialogAsync(IWavesDialog<object> view, IWavesDialogViewModel viewModel, bool addToHistory = true)
    {
        var region = await InitializeComponents(view, viewModel);

        void Action()
        {
            AddToHistoryStack(region, viewModel, addToHistory);
            _dialogSessions.Add(viewModel);
            NotifyDialogEvents();
            var contentControl = _contentControls[region];
            if (contentControl is WavesWindow window)
            {
                UnregisterView(contentControl);
                window.FrontContent = view;
                RegisterView(contentControl);
            }
            else
            {
                // TODO: what if another content control?
            }

            viewModel.RunPostInitializationAsync().FireAndForget();
        }

        if (!_contentControls.ContainsKey(region))
        {
            PendingActions.Add(region, Action);
        }
        else
        {
            await Dispatcher.UIThread.InvokeAsync(Action);
        }
    }

    /// <summary>
    /// Adds new window to content control dictionary.
    /// </summary>
    /// <param name="region">Region.</param>
    /// <param name="view">Content control.</param>
    private void AddContentControl(string region, ContentControl view)
    {
        if (!_contentControls.ContainsKey(region))
        {
            _contentControls.Add(region, view);
        }
        else
        {
            // rewrite if controls with same region are not equal.
            if (_contentControls[region].Equals(view))
            {
                return;
            }

            _contentControls[region] = view;
        }
    }

    /// <summary>
    /// Invokes <see cref="IWavesViewModel.ViewAppeared"/> for <see cref="StyledElement"/> is current <see cref="ContentControl"/>.
    /// </summary>
    /// <param name="control">Instance of <see cref="ContentControl"/>.</param>
    private void RegisterView(object? control)
    {
        if (control is not ContentControl contentControl)
        {
            return;
        }

        if (contentControl.DataContext is IWavesViewModel viewModel)
        {
            viewModel.ViewAppeared();
        }
    }

    /// <summary>
    /// Invokes <see cref="IWavesViewModel.ViewDisappeared"/> for <see cref="StyledElement"/> is current <see cref="ContentControl"/>.
    /// </summary>
    /// <param name="control">Instance of <see cref="ContentControl"/>.</param>
    private void UnregisterView(object? control)
    {
        if (control is not ContentControl contentControl)
        {
            return;
        }

        if (contentControl.DataContext is IWavesViewModel viewModel)
        {
            viewModel.ViewDisappeared();
        }

        if (contentControl.DataContext is IDisposable disposable)
        {
            disposable.Dispose();
        }

        if (contentControl is IWavesView view)
        {
            view.Dispose();
        }
    }
}
