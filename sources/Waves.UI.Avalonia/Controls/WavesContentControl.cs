using System;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Waves.UI.Avalonia.Helpers;

namespace Waves.UI.Avalonia.Controls
{
    /// <summary>
    ///     Waves content control.
    /// </summary>
    public class WavesContentControl : ContentControl, IDisposable
    {
        /// <summary>
        ///     Defines <see cref="Region" /> property.
        /// </summary>
        public static readonly StyledProperty<string> RegionProperty =
            AvaloniaProperty.Register<WavesContentControl, string>(
                nameof(Region));

        /// <summary>
        ///     Defines corner radius property.
        /// </summary>
        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<WavesContentControl, CornerRadius>(
                nameof(CornerRadius),
                new CornerRadius(3));

        private readonly List<IDisposable> _disposables;

        /// <summary>
        /// Creates new instance of <see cref="WavesContentControl"/>.
        /// </summary>
        public WavesContentControl()
        {
            _disposables = new List<IDisposable>();

            _disposables.Add(CornerRadiusProperty.Changed.Subscribe(x =>
                OnCornerRadiusChangedCallback(x.Sender, x.NewValue.GetValueOrDefault<StyledElement>())));
        }

        /// <summary>
        /// Finalizes <see cref="WavesContentControl"/>.
        /// </summary>
        ~WavesContentControl()
        {
            Dispose(true);
        }

        /// <summary>
        ///     Gets or sets center.
        /// </summary>
        [Category("Waves.UI SDK - Content")]
        public string Region
        {
            get => GetValue(RegionProperty);
            set => SetValue(RegionProperty, value);
        }

        /// <summary>
        ///     Gets or sets center.
        /// </summary>
        [Category("Waves.UI SDK - Appearance")]
        public CornerRadius CornerRadius
        {
            get => GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        ///     Callback when corner radius changed.
        /// </summary>
        /// <param name="d">Dependency object.</param>
        /// <param name="e">Arguments.</param>
        private static void OnCornerRadiusChangedCallback(
            IAvaloniaObject d,
            object e)
        {
            if (!(d is WavesContentControl control))
            {
                return;
            }

            control.SetValue(ControlHelper.CornerRadiusProperty, e);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes.
        /// </summary>
        /// <param name="disposing">Is disposing.</param>
        protected virtual void Dispose(
            bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}