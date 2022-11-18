using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using FluentAvalonia.UI.Controls;

namespace Waves.UI.Avalonia.Controls;

/// <summary>
/// Waves navigation control.
/// </summary>
public class WavesNavigationView :
    NavigationView,
    IStyledElement
{
    /// <summary>
    /// Creates new instance of <see cref="WavesNavigationView"/>.
    /// </summary>
    public WavesNavigationView()
    {
        Classes = Classes.Parse("waves-default");
    }

    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(NavigationView);
}
