using System;
using Avalonia.Styling;
using Waves.UI.Base.Enums;
using Waves.UI.Base.Interfaces;

namespace Waves.UI.Avalonia.Base
{
    /// <summary>
    ///     Color set.
    /// </summary>
    public class WavesThemeColorSet :
        Style,
        IWavesThemeColorSet
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public WavesThemeColorSetType Type { get; set; }
    }
}
