using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Waves.UI.Avalonia.Helpers;

namespace Waves.UI.Avalonia.Controls
{
    /// <summary>
    /// Button.
    /// </summary>
    public sealed class WavesTextBox :
        TextBox,
        IStyleable
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesTextBox"/>.
        /// </summary>
        public WavesTextBox()
        {
            Classes = Classes.Parse("waves-default");
        }

        /// <inheritdoc />
        Type IStyleable.StyleKey => typeof(TextBox);
    }
}
