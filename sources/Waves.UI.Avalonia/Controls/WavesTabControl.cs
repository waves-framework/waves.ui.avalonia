////using System;
////using System.Collections;
////using System.ComponentModel;
////using Waves.UI.Avalonia.Controls.Enums;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Waves tab control.
////    /// </summary>
////    public class WavesTabControl : TabControl
////    {
////        /// <summary>
////        /// Defines <see cref="Mode"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> ModeProperty = AvaloniaProperty.Register<,>(
////            "Mode",
////            typeof(WavesTabControlMode),
////            typeof(WavesDialog),
////            new FrameworkPropertyMetadata(WavesTabControlMode.Standard, FrameworkPropertyMetadataOptions.AffectsRender, OnModeChanged));

////        private IWavesCore _core;
////        private ContentTemplateSelector _selector;

////        /// <summary>
////        /// Gets or sets tab control work mode.
////        /// </summary>
////        [Category("Waves.UI - Content")]
////        public WavesTabControlMode Mode
////        {
////            get => (WavesTabControlMode)GetValue(ModeProperty);
////            set => SetValue(ModeProperty, value);
////        }

////        /// <summary>
////        /// Initializes content template selector.
////        /// </summary>
////        /// <param name="core">Core.</param>
////        public void InitializeSelector(IWavesCore core)
////        {
////            _core = core;

////            try
////            {
////                Dispatcher.Invoke(async () =>
////                {
////                    _selector = await _core.GetInstanceAsync<ContentTemplateSelector>();
////                    await _selector.InitializeAsync();

////                    if (_selector == null)
////                    {
////                        return;
////                    }

////                    UpdateSelector();
////                });
////            }
////            catch (Exception e)
////            {
////                _core.WriteLogAsync(e, _core).FireAndForget();
////            }
////        }

////        /// <inheritdoc />
////        protected override void OnItemsSourceChanged(
////            IEnumerable oldValue,
////            IEnumerable newValue)
////        {
////            UpdateSelector();

////            base.OnItemsSourceChanged(oldValue, newValue);
////        }

////        /// <summary>
////        /// Sets plugin template selector.
////        /// </summary>
////        protected void UpdateSelector()
////        {
////            ContentTemplateSelector = Mode == WavesTabControlMode.Standard ? null : _selector;

////            var selectedItem = SelectedItem;
////            SelectedItem = null;
////            SelectedItem = selectedItem;
////        }

////        /// <summary>
////        /// Callback when tab control work mode changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnModeChanged(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (d is not WavesTabControl tabControl)
////            {
////                return;
////            }

////            tabControl.UpdateSelector();
////        }
////    }
////}
