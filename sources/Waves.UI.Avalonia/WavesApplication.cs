using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Themes.Fluent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waves.Core;
using Waves.UI.Avalonia.Extensions;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia;

/// <summary>
/// Waves Avalonia application.
/// </summary>
public class WavesApplication : Application
{
    private bool _useDarkTheme = true;

    /// <summary>
    /// Gets core.
    /// </summary>
    protected WavesCore Core { get; private set; }

    /// <summary>
    /// Gets logger.
    /// </summary>
    protected ILogger<WavesApplication> Logger { get; set; }

    /// <summary>
    /// Gets navigation service.
    /// </summary>
    protected IWavesNavigationService NavigationService { get; private set; }

    /// <inheritdoc />
    public override void Initialize()
    {
        base.Initialize();

        TaskScheduler.UnobservedTaskException += OnTaskSchedulerUnobservedTaskException;

        Core = new WavesCore();
        Core.AddServices(ConfigureServices);
        Core.Start();
        Core.BuildContainer();

        Styles.Add(new FluentTheme(new Uri("avares://ControlCatalog/Styles"))
        {
        });
        this.AddStyle(Constants.GenericDictionaryUri);
        this.AddStyle(_useDarkTheme ? Constants.DefaultDarkColorsUri : Constants.DefaultLightColorsUri);

        Logger = Core.GetInstance<ILogger<WavesApplication>>();
        NavigationService = Core.GetInstance<IWavesNavigationService>();
    }

    /// <summary>
    /// Configure services for default Microsoft Extensions.
    /// </summary>
    /// <param name="services">Services collection.</param>
    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }

    /// <summary>
    ///     Notifies when unhandled exception received.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Arguments.</param>
    private void OnTaskSchedulerUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        Logger.LogError(e.Exception, "Application error occured");
        e.SetObserved();
    }
}
