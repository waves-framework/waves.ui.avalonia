using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;

namespace Waves.UI.Avalonia.Base
{
    /// <summary>
    /// Primary color set.
    /// </summary>
    public class PrimaryColorSet : UI.Services.PrimaryColorSet
    {
        private const string ColorKey = "Color";
        private const string ForegroundColorKey = "Color-Foreground";

        private readonly int[] _colorWeights = { 100, 200, 300, 400, 500, 600, 700, 800, 900 };
        private readonly int[] _foregroundColorWeights = { 100, 500, 900 };

        /// <summary>
        /// Creates new instance of <see cref="PrimaryColorSet"/>.
        /// </summary>
        /// <param name="id">Color set's id.</param>
        /// <param name="name">Color set's name.</param>
        /// <param name="styleInclude">Style include.</param>
        public PrimaryColorSet(Guid id, string name, StyleInclude styleInclude) : base(id, name)
        {
            StyleInclude = styleInclude;

            InitializeColors();
        }

        /// <summary>
        /// Gets color style include.
        /// </summary>
        public StyleInclude StyleInclude { get; private set; }

        /// <summary>
        /// Initializes colors.
        /// </summary>
        private void InitializeColors()
        {
            ColorDictionary.Clear();
            ForegroundColorDictionary.Clear();

            var colorDictionary = Extensions.StyleIncludeExtesions.GetColorsDictionary(StyleInclude, ColorKey, _colorWeights);
            var foregroundColorDictionary = Extensions.StyleIncludeExtesions.GetColorsDictionary(StyleInclude, ForegroundColorKey, _foregroundColorWeights);

            foreach (var pair in colorDictionary)
                ColorDictionary.Add(pair.Key, pair.Value);

            foreach (var pair in foregroundColorDictionary)
                ForegroundColorDictionary.Add(pair.Key, pair.Value);
        }
    }
}