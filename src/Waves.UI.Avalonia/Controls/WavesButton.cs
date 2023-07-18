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
    public sealed class WavesButton :
        Button,
        IStyleable
    {
        /// <summary>
        /// Defines <see cref="IsAccent"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsAccentProperty = AvaloniaProperty.Register<Button, bool>(nameof(IsAccent));

        /// <summary>
        /// Creates new instance of <see cref="WavesButton"/>.
        /// </summary>
        public WavesButton()
        {
            Classes = Classes.Parse("waves-default");
        }

        /// <summary>
        /// Gets or sets whether button is accent.
        /// </summary>
        [Category("Waves.UI SDK - Appearance")]
        public bool IsAccent
        {
            get => GetValue(IsAccentProperty);
            set
            {
                SetValue(IsAccentProperty, value);
                Classes = Classes.Parse(value ? "waves-accent" : "waves-default");
            }
        }

        /// <inheritdoc />
        Type IStyleable.StyleKey => typeof(Button);
    }
}
