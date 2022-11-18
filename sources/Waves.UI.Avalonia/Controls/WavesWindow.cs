using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using Waves.UI.Avalonia.Extensions;
using Waves.UI.Avalonia.Helpers;
using Waves.UI.Base.EventArgs;
using Waves.UI.Presentation.Interfaces.View.Controls;
using Waves.UI.Presentation.Interfaces.ViewModel;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Controls;

/// <summary>
/// Waves Avalonia Window.
/// </summary>
public class WavesWindow :
    Window,
    IWavesWindow<object>,
    IStyleable,
    IStyledElement
{
    /// <summary>
    ///     Defines <see cref="FrontContent" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<StyledElement> FrontContentProperty =
        AvaloniaProperty.Register<WavesWindow, StyledElement>(
            nameof(FrontContent));

    /// <summary>
    ///     Defines <see cref="CanGoBack" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<bool> CanGoBackProperty =
        AvaloniaProperty.Register<WavesWindow, bool>(
            nameof(CanGoBack));

    /// <summary>
    ///     Defines <see cref="GoBackCommand" /> dependency property.
    /// </summary>
    public static readonly StyledProperty<ICommand> GoBackCommandProperty =
        AvaloniaProperty.Register<WavesWindow, ICommand>(
            nameof(GoBackCommand));

    private readonly IWavesNavigationService _navigationService;

    private List<IDisposable> _disposables;
    private Dictionary<string, WavesContentControl> _regionContentControls;

    /// <summary>
    ///     Creates new instance of <see cref="WavesWindow" />.
    ///     Don't remove this constructor, because it needed for Avalonia.
    /// </summary>
    protected WavesWindow()
    {
    }

    /// <summary>
    ///     Creates new instance of <see cref="WavesWindow" />.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="navigationService">Instance of navigation service.</param>
    protected WavesWindow(
        ILogger<WavesWindow> logger,
        IWavesNavigationService navigationService)
    {
        Logger = logger;
        _navigationService = navigationService;
    }

    /// <inheritdoc />
    [Category("Waves.UI SDK - Navigation")]
    public bool CanGoBack
    {
        get => GetValue(CanGoBackProperty);
        set => SetValue(CanGoBackProperty, value);
    }

    /// <inheritdoc />
    [Category("Waves.UI SDK - Navigation")]
    public object? FrontContent
    {
        get => GetValue(FrontContentProperty);
        set => SetValue(FrontContentProperty, value);
    }

    /// <inheritdoc />
    [Category("Waves.UI SDK - Navigation")]
    public ICommand GoBackCommand
    {
        get => GetValue(GoBackCommandProperty);
        set => SetValue(GoBackCommandProperty, value);
    }

    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(WavesWindow);

    /// <summary>
    /// Gets logger.
    /// </summary>
    protected ILogger<WavesWindow> Logger { get; }

    /// <inheritdoc />
    public Task InitializeAsync()
    {
        try
        {
            _disposables = new List<IDisposable>();

            GoBackCommand = ReactiveCommand.CreateFromTask(OnGoBack);

            _disposables.Add(FrontContentProperty.Changed.Subscribe(x =>
                OnFrontLayerContentChangedCallback(x.Sender, x.NewValue.GetValueOrDefault<StyledElement>())));
            _disposables.Add(CanGoBackProperty.Changed.Subscribe(x =>
                OnCanGoBackChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>())));
            _disposables.Add(GoBackCommandProperty.Changed.Subscribe(x =>
                OnGoBackCommandChanged(x.Sender, x.NewValue.GetValueOrDefault<ICommand>())));

            //// TODO: initialize resource
            //// this.AddResource(Constants.GenericDictionaryUri);

            _regionContentControls = this.FindRegions(_navigationService, Logger);

            SubscribeEvents();
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An error occured while initializing window");
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
    protected virtual async void Dispose(
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
    ///     Callback when front layer content changed.
    /// </summary>
    /// <param name="d">Dependency object.</param>
    /// <param name="e">Arguments.</param>
    private static void OnFrontLayerContentChangedCallback(
        IAvaloniaObject d,
        object? e)
    {
        if (d is not WavesWindow window)
        {
            return;
        }

        window.SetValue(WindowHelper.FrontLayerContentProperty, e);
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
