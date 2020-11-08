using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;

namespace Waves.UI.Avalonia.Base
{
    /// <summary>
    /// Miscellaneous color set.
    /// </summary>
    public class MiscellaneousColorSet : UI.Services.ThemeService.ColorSets.MiscellaneousColorSet
    {
        /// <summary>
        /// Creates new instance of <see cref="MiscellaneousColorSet"/>.
        /// </summary>
        /// <param name="id">Color set's id.</param>
        /// <param name="name">Color set's name.</param>
        /// <param name="styleInclude">Style include.</param>
        public MiscellaneousColorSet(Guid id, string name, StyleInclude styleInclude) : base(id, name)
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

            var colorDictionary = Extensions.StyleIncludeExtesions.GetColorsDictionary(StyleInclude);
            var foregroundColorDictionary = Extensions.StyleIncludeExtesions.GetForegroundColorsDictionary(StyleInclude);

            foreach (var pair in colorDictionary)
                ColorDictionary.Add(pair.Key, pair.Value);

            foreach (var pair in foregroundColorDictionary)
                ForegroundColorDictionary.Add(pair.Key, pair.Value);
        }
    }
}