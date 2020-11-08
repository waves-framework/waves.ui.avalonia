using System;
using Avalonia.Controls;
using Avalonia.Media;
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
    public class AvaloniaDrawingElementView: Canvas, IDrawingElementPresenterView
    {
        /// <summary>
        ///     Creates new instance of <see cref="AvaloniaDrawingElementView" />.
        /// </summary>
        public AvaloniaDrawingElementView(IInputService inputService)
        {
            InitializeBehaviors(inputService);
            SubscribeEvents();

            Background = Brushes.Transparent;
        }

        /// <summary>
        ///     Finalizes instance.
        /// </summary>
        ~AvaloniaDrawingElementView()
        {
            Dispose();
        }

        /// <inheritdoc />
        public event EventHandler<IMessage> MessageReceived;
        
        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        ///     Notifies when message received.
        /// </summary>
        /// <param name="e">Message.</param>
        protected virtual void OnMessageReceived(IMessage e)
        {
            MessageReceived?.Invoke(this, e);
        }

        /// <summary>
        ///     Initializes behaviors.
        /// </summary>
        private void InitializeBehaviors(IInputService inputService)
        {
            Interaction.GetBehaviors(this)?.Add(new AvaloniaPaintBehavior(inputService));
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

        /// <inheritdoc />
        public void Dispose()
        {
            GC.SuppressFinalize(this);

            UnsubscribeEvents();
        }
    }
}