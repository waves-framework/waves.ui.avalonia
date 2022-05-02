using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using Microsoft.Extensions.Logging;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Avalonia.Services;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Extensions;

/// <summary>
/// Extensions for visual controls.
/// </summary>
internal static class VisualExtensions
{
    /// <summary>
    ///     Finds all controls by current type.
    /// </summary>
    /// <typeparam name="T">Current type.</typeparam>
    /// <param name="control">Control.</param>
    /// <returns>Collection of controls.</returns>
    public static IEnumerable<T> FindVisualChildren<T>(
        this IVisual control)
    {
        var content = control;
        if (content is ContentControl contentControl)
        {
            content = contentControl.Content as IVisual;
        }

        if (content == null)
        {
            yield break;
        }

        foreach (var child in content.VisualChildren)
        {
            if (child is T childType)
            {
                yield return childType;
            }

            foreach (var other in FindVisualChildren<T>(child))
            {
                yield return other;
            }
        }
    }

    /// <summary>
    ///     Finds all waves content control with regions in object.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="navigationService">Navigation service.</param>
    /// <param name="logger">Logger.</param>
    /// <returns>Regions / Control dictionary.</returns>
    public static Dictionary<string, WavesContentControl> FindRegions(
        this IVisual obj,
        IWavesNavigationService navigationService,
        ILogger logger = null!)
    {
        var controls = obj.FindVisualChildren<WavesContentControl>();

        var dictionary = new Dictionary<string, WavesContentControl>();

        foreach (var control in controls)
        {
            var region = control.GetValue(WavesContentControl.RegionProperty);

            if (string.IsNullOrEmpty(region))
            {
                continue;
            }

            dictionary.Add(region, control);

            navigationService.RegisterContentControl(region, control);

            logger?.LogDebug($"Content control {control.GetType()} registered with region {region}");
        }

        return dictionary;
    }
}
