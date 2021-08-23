using System;
using Avalonia.Styling;
using Waves.UI.Base.Enums;
using Waves.UI.Base.Interfaces;

namespace Waves.UI.Avalonia.Colors
{
    /// <summary>
    ///     Color set.
    /// </summary>
    public class ColorSet : Style, IThemeColorSet
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public ThemeColorSetType Type { get; set; }
    }
}