using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Threading;
using Avalonia.Xaml.Interactivity;
using Waves.Core.Base.Interfaces;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Avalonia.Controls.Drawing.Engines.Avalonia.Behavior;
using Waves.UI.Drawing.View.Interfaces;

namespace Waves.UI.Avalonia.Controls.Drawing.Engines.Avalonia.View
{
    /// <summary>
    ///     Drawing canvas.
    /// </summary>
    public sealed class AvaloniaSkiaDrawingElementView: Control, IDrawingElementPresenterView
    {
        /// <summary>
        ///     Creates new instance of <see cref="AvaloniaSkiaDrawingElementView" />.
        /// </summary>
        public AvaloniaSkiaDrawingElementView(IInputService inputService)
        {
            InitializeBehaviors(inputService);
            SubscribeEvents();

            //TODO Background = Brushes.Transparent;
        }

        /// <summary>
        ///     Finalizes instance.
        /// </summary>
        ~AvaloniaSkiaDrawingElementView()
        {
            Dispose();
        }

        /// <inheritdoc />
        public event EventHandler<IMessage> MessageReceived;

        /// <summary>
        /// Event for requetsing redraw.
        /// </summary>
        public event EventHandler RedrawRequested; 
        
        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();
        
        /// <summary>
        /// Gets or sets drawing context implementation.
        /// </summary>
        public AvaloniaSkiaDrawingOperation DrawOperation { get; protected set; }

        /// <inheritdoc />
        public void Dispose()
        {
            GC.SuppressFinalize(this);

            UnsubscribeEvents();
        }

        /// <inheritdoc />
        public override void Render(DrawingContext context)
        {
            DrawOperation = new AvaloniaSkiaDrawingOperation(Bounds);

            context.Custom(DrawOperation);
            
            //Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
            
            base.Render(context);

            OnRedrawRequested();
        }

        /// <summary>
        ///     Notifies when message received.
        /// </summary>
        /// <param name="e">Message.</param>
        private void OnMessageReceived(IMessage e)
        {
            MessageReceived?.Invoke(this, e);
        }

        /// <summary>
        ///     Initializes behaviors.
        /// </summary>
        private void InitializeBehaviors(IInputService inputService)
        {
            Interaction.GetBehaviors(this)?.Add(new AvaloniaSkiaPaintBehavior(inputService));
        }

        /// <summary>
        ///     Subscribes events.
        /// </summary>
        private void SubscribeEvents()
        {
        }

        /// <summary>
        ///     Unsubscribe events.
        /// </summary>
        private void UnsubscribeEvents()
        {
        }

        /// <summary>
        /// Actions when redraw requested.
        /// </summary>
        private void OnRedrawRequested()
        {
            RedrawRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}