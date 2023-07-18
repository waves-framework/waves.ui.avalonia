using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using Waves.UI.Avalonia.Extensions;
using Waves.UI.Presentation.Interfaces.View.Controls;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Controls;

/// <summary>
/// Waves Avalonia user control.
/// </summary>
public class WavesUserControl :
    UserControl,
    IWavesUserControl<object>,
    IStyleable
{
    private readonly ILogger<WavesUserControl> _logger;
    private readonly IWavesNavigationService _navigationService;

    private List<IDisposable> _disposables;
    private Dictionary<string, WavesContentControl> _regionContentControls;

    /// <summary>
    ///     Creates new instance of <see cref="WavesUserControl" />.
    ///     Don't remove this constructor, because it needed for Avalonia.
    /// </summary>
    protected WavesUserControl()
    {
    }

    /// <summary>
    ///     Creates new instance of <see cref="WavesUserControl" />.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="navigationService">Instance of navigation service.</param>
    protected WavesUserControl(
        ILogger<WavesUserControl> logger,
        IWavesNavigationService navigationService)
    {
        _logger = logger;
        _navigationService = navigationService;
    }

    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(UserControl);

    /// <inheritdoc />
    public Task InitializeAsync()
    {
        try
        {
            _disposables = new List<IDisposable>();
            _regionContentControls = this.FindRegions(_navigationService, _logger);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while initializing window");
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

        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }

        foreach (var control in _regionContentControls)
        {
            _navigationService.UnregisterContentControl(control.Key);
            _logger.LogDebug($"Control {control.Value} from region {control.Key} unregistered");
        }
    }
}
