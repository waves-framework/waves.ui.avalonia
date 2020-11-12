using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Avalonia.Controls.Drawing.Behavior;
using Waves.UI.Avalonia.Controls.Drawing.Engines.Avalonia.View;

namespace Waves.UI.Avalonia.Controls.Drawing.Engines.Avalonia.Behavior
{
    /// <summary>
    ///     Paint surface command behavior.
    /// </summary>
    public class AvaloniaSkiaPaintBehavior : PaintBehavior<Control>
    {
        /// <inheritdoc />  
        public AvaloniaSkiaPaintBehavior(IInputService inputService) 
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

            if (!(AssociatedObject is AvaloniaSkiaDrawingElementView view)) return;
            view.RedrawRequested += OnRedrawRequested;
        }
        
        /// <inheritdoc />
        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (!(AssociatedObject is AvaloniaSkiaDrawingElementView view)) return;
            view.RedrawRequested -= OnRedrawRequested;
        }

        /// <inheritdoc />  
        protected override void OnSizeChanged(double newWidth, double newHeight)
        {
            base.OnSizeChanged(newWidth, newHeight);
            Dispatcher.UIThread.InvokeAsync(Redraw);
        }

        /// <summary>
        ///     Actions when data context changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected override void OnDataContextChanged(object sender, EventArgs e)
        {
            base.OnDataContextChanged(sender, e);
            Dispatcher.UIThread.InvokeAsync(Redraw);
        }

        /// <summary>
        /// Notifies when redraw requested.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments/</param>
        protected override void OnRedrawRequested(object sender, EventArgs e)
        {
            base.OnRedrawRequested(sender, e);
            Dispatcher.UIThread.InvokeAsync(Redraw);
        }

        /// <summary>
        /// Redraws.
        /// </summary>
        private void Redraw()
        {
            if (!(AssociatedObject is AvaloniaSkiaDrawingElementView element)) return;
            if (element.DrawOperation?.Context == null) return;
            DataContext?.Draw(element.DrawOperation.Surface);
        }
    }
}