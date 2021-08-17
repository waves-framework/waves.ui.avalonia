using System;
using Avalonia.Controls;
using Avalonia.Styling;
using Waves.UI.Enums;

namespace Waves.UI.Avalonia.Colors
{
    /// <summary>
    /// Color set.
    /// </summary>
    public class ColorSet : Style
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets type.
        /// </summary>
        public ColorSetType Type { get; set; }
    }
}