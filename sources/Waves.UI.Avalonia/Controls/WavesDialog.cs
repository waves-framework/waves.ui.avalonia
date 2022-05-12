using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Styling;
using Waves.UI.Presentation.Interfaces.View.Controls;

namespace Waves.UI.Avalonia.Controls;

/// <summary>
/// Waves dialog.
/// </summary>
public class WavesDialog :
    ContentControl,
    IWavesContentControl<object>,
    IStyleable
{
    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(WavesDialog);

    /// <inheritdoc />
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public void Dispose()
    {
    }
}
