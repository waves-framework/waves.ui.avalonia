using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Waves.Core.Base.Interfaces;
using Waves.UI.Drawing.Charting.View.Interface;
using Waves.UI.Drawing.View.Interfaces;

namespace Waves.UI.Avalonia.Controls.Drawing.Charting.View
{
    /// <summary>
    ///     Chart control.
    /// </summary>
    public class ChartView : UserControl, IChartPresenterView
    {
        /// <summary>
        ///     Gets or sets "DrawingElementView".
        /// </summary>
        public static readonly StyledProperty<IDrawingElementPresenterView> DrawingElementViewProperty =
            AvaloniaProperty.Register<ChartView, IDrawingElementPresenterView>(
                "DrawingElementView");

        /// <summary>
        ///     Creates new instance of <see cref="ChartView" />.
        /// </summary>
        public ChartView()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        public event EventHandler<IWavesMessage> MessageReceived;

        /// <inheritdoc />
        public IWavesCore Core { get; protected set; }

        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        ///     Gets or sets Drawing element view.
        /// </summary>
        public IDrawingElementPresenterView DrawingElementView
        {
            get => GetValue(DrawingElementViewProperty);
            set
            {
                if (value.Equals(DrawingElementView)) return;

                SetValue(DrawingElementViewProperty, value);
            }
        }
        
        /// <inheritdoc />
        public void AttachCore(IWavesCore core)
        {
            Core = core;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DrawingElementView.Dispose();
        }

        /// <summary>
        ///     Initializes components.
        /// </summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}