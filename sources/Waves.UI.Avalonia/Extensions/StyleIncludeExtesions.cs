using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using Waves.Core.Base;
using Color = Avalonia.Media.Color;

namespace Waves.UI.Avalonia.Extensions
{
    /// <summary>
    /// Resource dictionary extensions.
    /// </summary>
    public static class StyleIncludeExtesions
    {
        /// <summary>
        ///     Gets colors dictionary from resource dictionary.
        /// </summary>
        /// <param name="styleInclude">Style include.</param>
        /// <param name="key">Key.</param>
        /// <param name="weights">Weight.</param>
        /// <returns>Colors dictionary.</returns>
        public static Dictionary<int, Waves.Core.Base.Color> GetColorsDictionary(this StyleInclude styleInclude, string key,
            int[] weights)
        {
            var result = new Dictionary<int, Waves.Core.Base.Color>();

            foreach (var weight in weights)
                result.Add(weight, GetColorFromResourceDictionary(styleInclude, key, weight));

            return result;
        }

        /// <summary>
        ///     Gets color from current style include by key and weight.
        /// </summary>
        /// <param name="styleInclude">Style include.</param>
        /// <param name="key">Key.</param>
        /// <param name="weight">Weight.</param>
        /// <returns>Color.</returns>
        public static Waves.Core.Base.Color GetColorFromResourceDictionary(this StyleInclude styleInclude, string key, int weight)
        {
            var currentKey = key + "-" + weight;

            var hasResource = styleInclude.TryGetResource(currentKey, out var obj);

            if (hasResource)
                if (obj is Color color)
                    return color.ToWavesColor();

            return new Waves.Core.Base.Color(0,0,0,0);
        }

        /// <summary>
        /// Gets string from current style include.
        /// </summary>
        /// <param name="styleInclude">Style include.</param>
        /// <param name="key">Key.</param>
        /// <returns>String.</returns>
        public static string GetString(this StyleInclude styleInclude, string key)
        {
            var hasResource = styleInclude.TryGetResource(key, out var obj);

            if (hasResource)
                if (obj is string value)
                    return value;

            return string.Empty;
        }

        /// <summary>
        /// Gets guid from current style include with parsing from string.
        /// </summary>
        /// <param name="styleInclude">Style include.</param>
        /// <param name="key">Key.</param>
        /// <returns>Guid.</returns>
        public static Guid GetGuidFromString(this StyleInclude styleInclude, string key)
        {
            var hasResource = styleInclude.TryGetResource(key, out var obj);

            if (hasResource)
            {
                if (obj is string value)
                {
                    var parsed = Guid.TryParse(value, out var guid);
                    if (parsed)
                        return guid;
                }
            }

            return Guid.Empty;
        }

        /// <summary>
        ///     Gets colors dictionary from resource dictionary.
        /// </summary>
        /// <param name="styleInclude">Style include.</param>
        /// <returns>Colors dictionary.</returns>
        public static Dictionary<string, Waves.Core.Base.Color> GetColorsDictionary(StyleInclude styleInclude)
        {
            var result = new Dictionary<string, Waves.Core.Base.Color>();

            //foreach (var key in dictionary.Keys)
            //{
            //    var str = key.ToString();
            //    if (str.EndsWith("-Color") && !str.EndsWith("-Foreground-Color"))
            //    {
            //        var color = (Color)dictionary[str];
            //        result.Add(str, color.ToWavesColor());
            //    }
            //}

            return result;
        }

        /// <summary>
        ///     Gets foreground colors dictionary from resource dictionary.
        /// </summary>
        /// <param name="styleInclude">Style include.</param>
        /// <returns>Colors dictionary.</returns>
        public static Dictionary<string, Waves.Core.Base.Color> GetForegroundColorsDictionary(StyleInclude styleInclude)
        {
            var result = new Dictionary<string, Waves.Core.Base.Color>();

            //foreach (var key in dictionary.Keys)
            //{
            //    var str = key.ToString();
            //    if (str.EndsWith("-Foreground-Color"))
            //    {
            //        var color = (Color)dictionary[str];
            //        result.Add(str, color.ToWavesColor());
            //    }
            //}

            return result;
        }
    }
}