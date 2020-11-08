using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Avalonia.Controls.Drawing.Behavior;

namespace Waves.UI.Avalonia.Controls.Drawing.Engines.Avalonia.Behavior
{
    /// <summary>
    ///     Paint surface command behavior.
    /// </summary>
    public class AvaloniaPaintBehavior : PaintBehavior<Canvas>
    {
        /// <inheritdoc />  
        public AvaloniaPaintBehavior(IInputService inputService) 
            : base(inputService)
        {
        }
        
        /// <inheritdoc />
        protected override void OnAttached()
        {
            base.OnAttached();
            
            if (AssociatedObject == null)
                return;
            
            AssociatedObject.Measure(Size.Infinity);
            AssociatedObject.InvalidateMeasure();
            AssociatedObject.InvalidateVisual();
        }

        /// <inheritdoc />  
        protected override void OnSizeChanged(double newWidth, double newHeight)
        {
            base.OnSizeChanged(newWidth, newHeight);
            DataContext?.Draw(AssociatedObject);
        }

        /// <summary>
        ///     Actions when data context changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected override void OnDataContextChanged(object sender, EventArgs e)
        {
            base.OnDataContextChanged(sender, e);
            DataContext?.Draw(AssociatedObject);
        }

        /// <summary>
        /// Notifies when redraw requested.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments/</param>
        protected override void OnRedrawRequested(object sender, EventArgs e)
        {
            base.OnRedrawRequested(sender, e);
            Dispatcher.UIThread.Post(() => { DataContext?.Draw(AssociatedObject); });
        }
    }
}