﻿using System;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;

namespace Waves.UI.Avalonia.Extensions;

/// <summary>
///     Tools for resources.
/// </summary>
public static class StyledElementExtensions
{
    /// <summary>
    ///     Adds resource to framework element.
    /// </summary>
    /// <param name="element">Framework element.</param>
    /// <param name="uri">URI.</param>
    public static void AddResource(this StyledElement element, string uri)
    {
        var resources = element.Resources;
        var dictionaries = resources.MergedDictionaries;
        var styleInclude = GetStyle(uri);
        dictionaries.Add(styleInclude);
    }

    /// <summary>
    ///     Adds resource to framework element.
    /// </summary>
    /// <param name="element">Framework element.</param>
    /// <param name="uri">URI.</param>
    public static void AddStyle(this Application element, string uri)
    {
        var styles = element.Styles;
        var styleInclude = GetStyle(uri);
        styles.Add(styleInclude);
    }

    /// <summary>
    ///     Creates style for current URI.
    /// </summary>
    /// <param name="uriString">URI.</param>
    /// <returns>Style.</returns>
    private static StyleInclude GetStyle(string uriString)
    {
        var uri = new Uri(uriString);
        return new StyleInclude(uri)
        {
            Source = uri,
        };
    }
}
