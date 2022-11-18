using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Waves.UI.Presentation.Interfaces.View.Controls;

namespace Waves.UI.Avalonia.Controls
{
    /// <summary>
    ///     Waves content control.
    /// </summary>
    public class WavesContentControl :
        ContentControl,
        IWavesContentControl<object>,
        IStyleable
    {
        /// <summary>
        ///     Defines <see cref="Region" /> property.
        /// </summary>
        public static readonly StyledProperty<string> RegionProperty =
            AvaloniaProperty.Register<WavesContentControl, string>(
                nameof(Region));

        /// <summary>
        ///     Gets or sets center.
        /// </summary>
        [Category("Waves.UI SDK - Regions")]
        public string Region
        {
            get => GetValue(RegionProperty);
            set => SetValue(RegionProperty, value);
        }

        /// <inheritdoc />
        Type IStyleable.StyleKey => typeof(ContentControl);

        /// <inheritdoc />
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }
    }
}
