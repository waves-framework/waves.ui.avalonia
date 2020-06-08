using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media;

namespace Waves.UI.Avalonia.Extensions
{
    /// <summary>
    /// Resource dictionary extensions.
    /// </summary>
    public static class ResourceDictionaryExtensions
    {
        /// <summary>
        ///     Gets colors dictionary from resource dictionary.
        /// </summary>
        /// <param name="dictionary">Resource dictionary.</param>
        /// <param name="key">Key.</param>
        /// <param name="weights">Weight.</param>
        /// <returns>Colors dictionary.</returns>
        public static Dictionary<int, Waves.Core.Base.Color> GetColorsDictionary(this ResourceDictionary dictionary, string key,
            int[] weights)
        {
            var result = new Dictionary<int, Waves.Core.Base.Color>();

            foreach (var weight in weights)
                result.Add(weight, GetColorFromResourceDictionary(dictionary, key, weight));

            return result;
        }

        /// <summary>
        ///     Gets color from resource dictionary by key and weight.
        /// </summary>
        /// <param name="dictionary">Resource dictionary.</param>
        /// <param name="key">Key.</param>
        /// <param name="weight">Weight.</param>
        /// <returns>Color.</returns>
        public static Waves.Core.Base.Color GetColorFromResourceDictionary(this ResourceDictionary dictionary, string key, int weight)
        {
            var currentKey = key + "-" + weight;

            var color = (Color)dictionary[currentKey];

            return color.ToWavesColor();
        }

        /// <summary>
        ///     Gets colors dictionary from resource dictionary.
        /// </summary>
        /// <param name="dictionary">ResourceDictionary</param>
        /// <returns>Colors dictionary.</returns>
        public static Dictionary<string, Waves.Core.Base.Color> GetColorsDictionary(ResourceDictionary dictionary)
        {
            var result = new Dictionary<string, Waves.Core.Base.Color>();

            foreach (var key in dictionary.Keys)
            {
                var str = key.ToString();
                if (str.EndsWith("-Color") && !str.EndsWith("-Foreground-Color"))
                {
                    var color = (Color)dictionary[str];
                    result.Add(str, color.ToWavesColor());
                }
            }

            return result;
        }

        /// <summary>
        ///     Gets foreground colors dictionary from resource dictionary.
        /// </summary>
        /// <param name="dictionary">ResourceDictionary</param>
        /// <returns>Colors dictionary.</returns>
        public static Dictionary<string, Waves.Core.Base.Color> GetForegroundColorsDictionary(ResourceDictionary dictionary)
        {
            var result = new Dictionary<string, Waves.Core.Base.Color>();

            foreach (var key in dictionary.Keys)
            {
                var str = key.ToString();
                if (str.EndsWith("-Foreground-Color"))
                {
                    var color = (Color)dictionary[str];
                    result.Add(str, color.ToWavesColor());
                }
            }

            return result;
        }
    }
}