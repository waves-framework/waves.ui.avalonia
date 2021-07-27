using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Styling;
using Avalonia.Threading;
using Waves.Core.Base.Interfaces;
using Waves.Core.Extensions;
using Waves.UI.Avalonia.Controls.Enums;
using Waves.UI.Avalonia.Selectors;

namespace Waves.UI.Avalonia.Controls
{
    /// <summary>
    /// Waves tab control.
    /// </summary>
    public class WavesTabControl : TabControl
    {
        /// <summary>
        /// Defines <see cref="Mode"/> property.
        /// </summary>
        public static readonly StyledProperty<WavesTabControlMode> ModeProperty = AvaloniaProperty.Register <WavesTabControl, WavesTabControlMode>(
            "Mode");

        private readonly List<IDisposable> _disposables;

        private IWavesCore _core;
        private ContentTemplateSelector _selector;

        /// <summary>
        /// Creates new instance of <see cref="WavesTabControl"/>
        /// </summary>
        public WavesTabControl()
        {
            _disposables = new List<IDisposable>();
        }
        
        /// <summary>
        /// Gets or sets tab control work mode.
        /// </summary>
        [Category("Waves.UI - Content")]
        public WavesTabControlMode Mode
        {
            get => GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        /// <inheritdoc />
        protected override void OnAttachedToVisualTree(
            VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            SubscribeEvents();
        }

        /// <inheritdoc />
        protected override void OnDetachedFromLogicalTree(
            LogicalTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromLogicalTree(e);
            UnsubscribeEvents();
        }

        /// <summary>
        /// Initializes content template selector.
        /// </summary>
        /// <param name="core">Core.</param>
        public void InitializeSelector(IWavesCore core)
        {
            _core = core;

            try
            {
                Dispatcher.UIThread.Post(async () =>
                {
                    _selector = await _core.GetInstanceAsync<ContentTemplateSelector>();
                    await _selector.InitializeAsync();

                    if (_selector == null)
                    {
                        return;
                    }

                    UpdateSelector();
                });
            }
            catch (Exception e)
            {
                _core.WriteLogAsync(e, _core).FireAndForget();
            }
        }

        /// <inheritdoc />
        protected override void ItemsCollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs e)
        {
            UpdateSelector();

            base.ItemsCollectionChanged(sender, e);
        }

        /// <summary>
        /// Sets plugin template selector.
        /// </summary>
        protected void UpdateSelector()
        {
            if (Mode == WavesTabControlMode.Plugins)
            {
                if (!DataTemplates.Contains(_selector))
                {
                    DataTemplates.Add(_selector);
                }
            }
            else
            {
                if (DataTemplates.Contains(_selector))
                {
                    DataTemplates.Remove(_selector);
                }
            }
            
            var selectedItem = SelectedItem;
            SelectedItem = null;
            SelectedItem = selectedItem;
        }

        /// <summary>
        /// Subscribes to events.
        /// </summary>
        private void SubscribeEvents()
        {
            _disposables.Add(ModeProperty.Changed.Subscribe(x =>
                OnModeChangedCallback(x.Sender, x.NewValue.GetValueOrDefault<WavesTabControlMode>())));
        }

        /// <summary>
        /// Unsubscribe from events.
        /// </summary>
        private void UnsubscribeEvents()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// Callback when tab control work mode changed.
        /// </summary>
        /// <param name="d">Dependency object.</param>
        /// <param name="e">Arguments.</param>
        private static void OnModeChangedCallback(
            IAvaloniaObject d,
            object e)
        {
            if (d is not WavesTabControl tabControl)
            {
                return;
            }

            tabControl.UpdateSelector();
        }
    }
}
