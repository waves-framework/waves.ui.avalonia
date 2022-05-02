using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Waves.Core;
using Waves.Core.Base.Attributes;
using Waves.Core.Base.Enums;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.EventArgs;
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

    /// <summary>
    /// Creates new instance of <see cref="WavesNavigationService"/>.
    /// </summary>
    /// <param name="core">Core.</param>
    /// <param name="configuration">Configuration.</param>
    /// <param name="logger">Logger.</param>
    public WavesNavigationService(
        WavesCore core,
        IConfiguration configuration,
        ILogger<WavesNavigationService> logger)
        : base(core, configuration, logger)
    {
        _contentControls = new Dictionary<string, ContentControl>();
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
        var region = await InitializeComponents(view, viewModel);
        var contentControl = view as ContentControl;
        if (contentControl == null)
        {
            return;
        }

        void Action()
        {
            view.Show();
            RegisterView(contentControl);
            Logger.LogDebug($"Navigation to view {view.GetType()} with data context {viewModel.GetType()} in region {region} completed");
        }

        await Dispatcher.UIThread.InvokeAsync(Action);

        AddContentControl(region, contentControl);
    }

    /// <inheritdoc />
    protected override async Task InitializePageAsync(IWavesPage<object> view, IWavesViewModel viewModel, bool addToHistory = true)
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

            Logger.LogDebug($"Navigation to view {view.GetType()} with data context {viewModel.GetType()} in region {region} completed");
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
            UnregisterView(contentControl);
            _contentControls[region].Content = view;
            RegisterView(contentControl);

            Logger.LogDebug($"Navigation to view {view.GetType()} with data context {viewModel.GetType()} in region {region} completed");
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
    protected override Task InitializeDialogAsync(IWavesDialog<object> view, IWavesDialogViewModel viewModel, bool addToHistory = true)
    {
        throw new System.NotImplementedException();
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
