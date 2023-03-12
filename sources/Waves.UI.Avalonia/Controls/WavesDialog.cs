using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Styling;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using Waves.UI.Avalonia.Extensions;
using Waves.UI.Avalonia.Helpers;
using Waves.UI.Base.EventArgs;
using Waves.UI.Dialogs.Interfaces;
using Waves.UI.Presentation.Interfaces.View.Controls;
using Waves.UI.Presentation.Interfaces.ViewModel;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Controls;

/// <summary>
/// Waves dialog.
/// </summary>
public class WavesDialog :
    ContentControl,
    IWavesDialog<object>,
    IStyleable
{
    /// <summary>
    ///     Defines <see cref="CanGoBack" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<bool> CanGoBackProperty =
        AvaloniaProperty.Register<WavesDialog, bool>(
            nameof(CanGoBack));

    /// <summary>
    ///     Defines <see cref="GoBackCommand" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<ICommand> GoBackCommandProperty =
        AvaloniaProperty.Register<WavesDialog, ICommand>(
            nameof(GoBackCommand));

    /// <summary>
    ///     Defines <see cref="Title" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<WavesDialog, string>(
            nameof(Title));

    /// <summary>
    ///     Defines <see cref="Tools" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<IEnumerable<IWavesDialogTool>> ToolsProperty =
        AvaloniaProperty.Register<WavesDialog, IEnumerable<IWavesDialogTool>>(
            nameof(Tools));

    /// <summary>
    ///     Defines <see cref="DoneCommand" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<ICommand> DoneCommandProperty =
        AvaloniaProperty.Register<WavesDialog, ICommand>(
            nameof(DoneCommand));

    /// <summary>
    ///     Defines <see cref="DoneButtonCaption" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<string> DoneButtonCaptionProperty =
        AvaloniaProperty.Register<WavesDialog, string>(
            nameof(DoneButtonCaption));

    /// <summary>
    ///     Defines <see cref="IsDoneButtonVisible" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<bool> IsDoneButtonVisibleProperty =
        AvaloniaProperty.Register<WavesDialog, bool>(
            nameof(IsDoneButtonVisible));

    /// <summary>
    ///     Defines <see cref="CancelCommand" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<ICommand> CancelCommandProperty =
        AvaloniaProperty.Register<WavesDialog, ICommand>(
            nameof(CancelCommand));

    /// <summary>
    ///     Defines <see cref="CancelButtonCaption" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<string> CancelButtonCaptionProperty =
        AvaloniaProperty.Register<WavesDialog, string>(
            nameof(CancelButtonCaption));

    /// <summary>
    ///     Defines <see cref="IsCancelButtonVisible" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<bool> IsCancelButtonVisibleProperty =
        AvaloniaProperty.Register<WavesDialog, bool>(
            nameof(IsCancelButtonVisible));

    private readonly IWavesNavigationService _navigationService;

    private List<IDisposable> _disposables;
    private Dictionary<string, WavesContentControl> _regionContentControls;

    /// <summary>
    ///     Creates new instance of <see cref="WavesWindow" />.
    ///     Don't remove this constructor, because it needed for Avalonia.
    /// </summary>
    protected WavesDialog()
    {
    }

    /// <summary>
    ///     Creates new instance of <see cref="WavesWindow" />.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="navigationService">Instance of navigation service.</param>
    protected WavesDialog(
        ILogger<WavesDialog> logger,
        IWavesNavigationService navigationService)
    {
        Logger = logger;
        _navigationService = navigationService;
    }

    /// <summary>
    /// Gets or sets can navigation go back or not.
    /// </summary>
    public bool CanGoBack
    {
        get => GetValue(CanGoBackProperty);
        set => SetValue(CanGoBackProperty, value);
    }

    /// <summary>
    /// Gets or sets go back command.
    /// </summary>
    public ICommand GoBackCommand
    {
        get => GetValue(GoBackCommandProperty);
        set => SetValue(GoBackCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets dialog title.
    /// </summary>
    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets dialog tools.
    /// </summary>
    public IEnumerable<IWavesDialogTool> Tools
    {
        get => GetValue(ToolsProperty);
        set => SetValue(ToolsProperty, value);
    }

    /// <summary>
    /// Gets or sets done command.
    /// </summary>
    public ICommand DoneCommand
    {
        get => GetValue(DoneCommandProperty);
        set => SetValue(DoneCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets done button caption.
    /// </summary>
    public string DoneButtonCaption
    {
        get => GetValue(DoneButtonCaptionProperty);
        set => SetValue(DoneButtonCaptionProperty, value);
    }

    /// <summary>
    /// Gets or sets done button visibility.
    /// </summary>
    public bool IsDoneButtonVisible
    {
        get => GetValue(IsDoneButtonVisibleProperty);
        set => SetValue(IsDoneButtonVisibleProperty, value);
    }

    /// <summary>
    /// Gets or sets cancel command.
    /// </summary>
    public ICommand CancelCommand
    {
        get => GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets cancel button caption.
    /// </summary>
    public string CancelButtonCaption
    {
        get => GetValue(CancelButtonCaptionProperty);
        set => SetValue(CancelButtonCaptionProperty, value);
    }

    /// <summary>
    /// Gets or sets cancel button visibility.
    /// </summary>
    public bool IsCancelButtonVisible
    {
        get => GetValue(IsCancelButtonVisibleProperty);
        set => SetValue(IsCancelButtonVisibleProperty, value);
    }

    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(WavesDialog);

    /// <summary>
    /// Gets logger.
    /// </summary>
    protected ILogger<WavesDialog> Logger { get; }

    /// <inheritdoc />
    public Task InitializeAsync()
    {
        try
        {
            _disposables = new List<IDisposable>();

            GoBackCommand = ReactiveCommand.CreateFromTask(OnGoBack);

            _disposables.Add(CanGoBackProperty.Changed.Subscribe(x =>
                OnCanGoBackChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>())));
            _disposables.Add(GoBackCommandProperty.Changed.Subscribe(x =>
                OnGoBackCommandChanged(x.Sender, x.NewValue.GetValueOrDefault<ICommand>())));

            _regionContentControls = this.FindRegions(_navigationService, Logger);

            SubscribeEvents();
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An error occured while initializing dialog");
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Disposes object.
    /// </summary>
    /// <param name="disposing">
    ///     Set
    ///     <value>true</value>
    ///     if you need to release managed and unmanaged resources. Set
    ///     <value>false</value>
    ///     if need to release only unmanaged resources.
    /// </param>
    protected void Dispose(
        bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        UnsubscribeEvents();

        foreach (var control in _regionContentControls)
        {
            _navigationService.UnregisterContentControl(control.Key);
            Logger.LogDebug($"Control {control.Value} from region {control.Key} unregistered");
        }
    }

    /// <summary>
    ///     Callback when <see cref="CanGoBackProperty" /> changed.
    /// </summary>
    /// <param name="d">Dependency object.</param>
    /// <param name="e">Arguments.</param>
    private static void OnCanGoBackChanged(
        IAvaloniaObject d,
        object e)
    {
        if (d is not WavesWindow window)
        {
            return;
        }

        window.SetValue(WindowHelper.CanGoBackProperty, e);
    }

    /// <summary>
    ///     Callback when <see cref="GoBackCommandProperty" /> changed.
    /// </summary>
    /// <param name="d">Dependency object.</param>
    /// <param name="e">Arguments.</param>
    private static void OnGoBackCommandChanged(
        IAvaloniaObject d,
        object? e)
    {
        if (d is not WavesWindow window)
        {
            return;
        }

        window.SetValue(WindowHelper.GoBackCommandProperty, e);
    }

    /// <summary>
    ///     Actions when need to go back.
    /// </summary>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    private async Task OnGoBack()
    {
        if (Content is not StyledElement element)
        {
            return;
        }

        if (element.DataContext is not IWavesViewModel viewModel)
        {
            return;
        }

        await _navigationService.GoBackAsync(viewModel);
    }

    /// <summary>
    /// Actions when navigation "Go back" state changed.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Arguments.</param>
    private void OnNavigationServiceGoBackChanged(object? sender, GoBackNavigationEventArgs e)
    {
        if (!e.ContentControl.Equals(this))
        {
            return;
        }

        CanGoBack = e.CanGoBack;
    }

    /// <summary>
    ///     Subscribes to events.
    /// </summary>
    private void SubscribeEvents()
    {
        _navigationService.GoBackChanged += OnNavigationServiceGoBackChanged;
    }

    /// <summary>
    ///     Unsubscribes from events.
    /// </summary>
    private void UnsubscribeEvents()
    {
        _navigationService.GoBackChanged -= OnNavigationServiceGoBackChanged;

        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}
