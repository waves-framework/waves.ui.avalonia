////using System;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Threading.Tasks;
////using Waves.UI.Presentation.Interfaces;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Waves drawing surface.
////    /// </summary>
////    public class WavesSurface : ContentControl, IWavesView
////    {
////        /// <summary>
////        /// Defines corner radius property.
////        /// </summary>
////        public static readonly StyledProperty<> CornerRadiusProperty = AvaloniaProperty.Register<,>(
////            nameof(CornerRadius),
////            typeof(CornerRadius),
////            typeof(WavesSurface),
////            new FrameworkPropertyMetadata(new CornerRadius(3), FrameworkPropertyMetadataOptions.AffectsRender, OnCornerRadiusChangedCallback));

////        /// <summary>
////        /// Defines <see cref="DrawingObjects"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> DrawingObjectsProperty = AvaloniaProperty.Register<,>(
////            nameof(DrawingObjects),
////            typeof(IEnumerable<IWavesDrawingObject>),
////            typeof(WavesSurface),
////            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnDrawingObjectsChanged));

////        private IWavesCore _core;

////        /// <inheritdoc />
////        public event PropertyChangedEventHandler PropertyChanged;

////        /// <inheritdoc />
////        public event PropertyChangingEventHandler PropertyChanging;

////        /// <summary>
////        /// Gets or sets drawing objects.
////        /// </summary>
////        [Category("Waves.UI SDK - Content")]
////        public IEnumerable<IWavesDrawingObject> DrawingObjects
////        {
////            get => (IEnumerable<IWavesDrawingObject>)GetValue(DrawingObjectsProperty);
////            set => SetValue(DrawingObjectsProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets center.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public CornerRadius CornerRadius
////        {
////            get => (CornerRadius)GetValue(CornerRadiusProperty);
////            set => SetValue(CornerRadiusProperty, value);
////        }

////        /// <inheritdoc />
////        public virtual void RaisePropertyChanging(PropertyChangingEventArgs args)
////        {
////            OnPropertyChanging(args);
////        }

////        /// <inheritdoc />
////        public virtual void RaisePropertyChanged(PropertyChangedEventArgs args)
////        {
////            OnPropertyChanged(args);
////        }

////        /// <inheritdoc />
////        public void Dispose()
////        {
////            Dispose(true);
////            GC.SuppressFinalize(this);
////        }

////        /// <inheritdoc />
////        public virtual Task InitializeAsync()
////        {
////            return Task.CompletedTask;
////        }

////        /// <summary>
////        /// Initializes content template selector.
////        /// </summary>
////        /// <param name="core">Core.</param>
////        public async void InitializeElement(IWavesCore core)
////        {
////            _core = core;

////            try
////            {
////                var service = await _core.GetInstanceAsync<IWavesDrawingElementService>();
////                var element = await service.GetDrawingElement();
////                await element.InitializeAsync();
////                element.DrawingObjects = DrawingObjects;
////                Content = element;
////            }
////            catch (Exception e)
////            {
////                _core.WriteLogAsync(e, _core).FireAndForget();
////            }
////        }

////        /// <summary>
////        ///     Disposes object.
////        /// </summary>
////        /// <param name="disposing">Set
////        ///     <value>true</value>
////        ///     if you need to release managed and unmanaged resources. Set
////        ///     <value>false</value>
////        ///     if need to release only unmanaged resources.
////        /// </param>
////        protected virtual void Dispose(bool disposing)
////        {
////            if (disposing)
////            {
////                // TODO: your code for release managed resources.
////            }

////            // TODO: your code for release unmanaged resources.
////        }

////        /// <summary>
////        /// Callback when property changed.
////        /// </summary>
////        /// <param name="e">Arguments.</param>
////        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
////        {
////            PropertyChanged?.Invoke(this, e);
////        }

////        /// <summary>
////        /// Callback when property changing.
////        /// </summary>
////        /// <param name="e">Arguments.</param>
////        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
////        {
////            PropertyChanging?.Invoke(this, e);
////        }

////        /// <summary>
////        /// Callback when corner radius changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnCornerRadiusChangedCallback(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (!(d is WavesSurface surface))
////            {
////                return;
////            }

////            surface.SetValue(ControlHelper.CornerRadiusProperty, e.NewValue);
////        }

////        /// <summary>
////        /// Callback when drawing objects changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnDrawingObjectsChanged(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (!(d is WavesSurface surface))
////            {
////                return;
////            }

////            if (!(surface.Content is IWavesDrawingElement element))
////            {
////                return;
////            }

////            element.DrawingObjects = (IEnumerable<IWavesDrawingObject>)e.NewValue;
////        }
////    }
////}
