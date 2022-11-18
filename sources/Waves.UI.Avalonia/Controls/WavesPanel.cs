using System;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Styling;

namespace Waves.UI.Avalonia.Controls;

/// <summary>
/// Waves panel.
/// </summary>
[DefaultProperty(nameof(Content))]
public sealed class WavesPanel :
    UserControl,
    IStyleable
{
    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(WavesPanel);
}
