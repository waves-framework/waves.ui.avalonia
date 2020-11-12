using Avalonia;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace Waves.UI.Avalonia.Controls.Drawing.Engines.Avalonia.View
{
    /// <summary>
    /// Custom drawing operation for avalonia skia drawing.
    /// </summary>
    public class AvaloniaSkiaDrawingOperation : ICustomDrawOperation
    {
        /// <summary>
        /// Creates new instance of <see cref="AvaloniaSkiaDrawingOperation"/>.
        /// </summary>
        /// <param name="bounds">Bounds.</param>
        public AvaloniaSkiaDrawingOperation(Rect bounds)
        {
            Bounds = bounds;
        }
    
        /// <summary>
        /// Gets drawing context.
        /// </summary>
        public ISkiaDrawingContextImpl Context { get; protected set; }
        
        /// <summary>
        /// Gets surface.
        /// </summary>
        public SKSurface Surface { get; protected set; }
        
        /// <inheritdoc />
        public Rect Bounds { get; }
        
        /// <inheritdoc />
        public bool HitTest(Point p) => true;
        
        /// <inheritdoc />
        public bool Equals(ICustomDrawOperation other) => false;
        
        /// <inheritdoc />
        public void Render(IDrawingContextImpl context)
        {
            if (context == null)
                return;
            
            Context = context as ISkiaDrawingContextImpl;
            Surface = Context?.SkSurface;
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }
    }
}