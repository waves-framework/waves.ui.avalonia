using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;

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
        ///     Gets color from resource dictionary by key and weight.
        /// </summary>
        /// <param name="styleInclude">Style include.</param>
        /// <param name="key">Key.</param>
        /// <param name="weight">Weight.</param>
        /// <returns>Color.</returns>
        public static Waves.Core.Base.Color GetColorFromResourceDictionary(this StyleInclude styleInclude, string key, int weight)
        {
            var currentKey = key + "-" + weight;

            var color = (Color) styleInclude.FindResource(currentKey);

            return color.ToWavesColor();
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