using System;
using System.Composition;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Avalonia.Controls.Drawing.Engines.Avalonia.View;
using Waves.UI.Drawing.Base.Interfaces;
using Waves.UI.Drawing.View.Interfaces;
using Object = Waves.Core.Base.Object;

namespace Waves.UI.Avalonia.Controls.Drawing.Engines.Avalonia
{
    /// <summary>
    ///     Engine.
    /// </summary>
    [Export(typeof(IDrawingEngine))]
    public class Engine : Object, IDrawingEngine
    {
        /// <inheritdoc />
        public override Guid Id { get; } = Guid.Parse("3D0E4352-7ED9-40D8-976E-0582F350C225");

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public override string Name { get; set; } = "Avalonia Drawing Engine";

        /// <inheritdoc />
        public IDrawingElementPresenterView GetView(IInputService inputService)
        {
            return new AvaloniaDrawingElementView(inputService);
        }

        /// <inheritdoc />
        public IDrawingElement GetDrawingElement()
        {
            return new AvaloniaDrawingElement();
        }
        
        /// <inheritdoc />
        public override void Dispose()
        {
            // TODO: ???.
        }
    }
}