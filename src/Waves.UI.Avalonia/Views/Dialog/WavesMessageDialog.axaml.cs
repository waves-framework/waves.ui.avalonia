using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;
using Waves.UI.Dialogs;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Views.Dialog;

/// <summary>
/// Message dialog view.
/// </summary>
[WavesView(typeof(WavesMessageDialogViewModel))]
public partial class WavesMessageDialog : WavesDialog
{
    /// <summary>
    /// Creates new instance os <see cref="WavesMessageDialog"/>.
    /// </summary>
    public WavesMessageDialog()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Creates new instance of <see cref="WavesMessageDialog"/>.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="navigationService">Navigation service.</param>
    public WavesMessageDialog(
        ILogger<WavesMessageDialog> logger,
        IWavesNavigationService navigationService)
        : base(logger, navigationService)
    {
        InitializeComponent();
    }

    /// <summary>
    /// Initializes components.
    /// </summary>
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
