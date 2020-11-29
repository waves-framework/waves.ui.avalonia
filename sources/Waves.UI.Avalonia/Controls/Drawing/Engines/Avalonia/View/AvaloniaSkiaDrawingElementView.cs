using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Skia;
using Avalonia.Threading;
using Avalonia.Visuals.Media.Imaging;
using Avalonia.Xaml.Interactivity;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Avalonia.Controls.Drawing.Engines.Avalonia.Behavior;
using Waves.UI.Drawing.View.Interfaces;

namespace Waves.UI.Avalonia.Controls.Drawing.Engines.Avalonia.View
{
    /// <summary>
    ///     Drawing canvas.
    /// </summary>
    public sealed class AvaloniaSkiaDrawingElementView : UserControl, IDrawingElementPresenterView
    {
        /// <summary>
        ///     Creates new instance of <see cref="AvaloniaSkiaDrawingElementView" />.
        /// </summary>
        public AvaloniaSkiaDrawingElementView(IInputService inputService)
        {
            InitializeBehaviors(inputService);
            SubscribeEvents();

            Canvas = new Canvas();
            Content = Canvas;
        }
        
        /// <summary>
        /// Gets whether is rendering running.
        /// </summary>
        public bool IsRendering { get; set; }

        /// <inheritdoc />
        public event EventHandler<IWavesMessage> MessageReceived;
        
        /// <summary>
        /// Gets canvas.
        /// </summary>
        public Canvas Canvas { get; private set; }
        
        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();
        
        /// <inheritdoc />
        public IWavesCore Core { get; private set; }

        /// <summary>
        ///     Gets render target as bitmap.
        /// </summary>
        public RenderTargetBitmap RenderTarget { get; set; }

        /// <summary>
        ///     Gets skia drawing context.
        /// </summary>
        public ISkiaDrawingContextImpl DrawingContext { get; private set; }

        /// <inheritdoc />
        public void Dispose()
        {
            RenderTarget?.Dispose();
            DrawingContext?.Dispose();

            GC.SuppressFinalize(this);

            UnsubscribeEvents();
        }

        /// <inheritdoc />
        public void AttachCore(IWavesCore core)
        {
            Core = core;
        }

        /// <summary>
        ///     Finalizes instance.
        /// </summary>
        ~AvaloniaSkiaDrawingElementView()
        {
            Dispose();
        }

        /// <summary>
        ///     Event for requesting redraw.
        /// </summary>
        public event EventHandler RedrawRequested;

        /// <summary>
        ///     Initializes control.
        /// </summary>
        public void Initialize()
        {
            try
            {
                RenderTarget?.Dispose();
                DrawingContext?.Dispose();

                var width = Bounds.Width;
                var height = Bounds.Height;

                if (Math.Abs(width) < 1 || Math.Abs(height) < 1)
                    return;

                if (double.IsNaN(width))
                    return;

                if (double.IsNaN(height))
                    return;

                RenderTarget = new RenderTargetBitmap(
                    new PixelSize(
                        (int) width,
                        (int) height),
                    new Vector(96, 96));

                if (RenderTarget == null)
                {
                    OnMessageReceived(new WavesMessage(
                        "Initialization",
                        "Render target was not initialized.",
                        Name,
                        WavesMessageType.Warning));

                    return;
                }

                var skiaContext = RenderTarget.CreateDrawingContext(null);

                if (skiaContext == null)
                {
                    OnMessageReceived(new WavesMessage(
                        "Initialization",
                        "Skia drawing context was not created.",
                        Name,
                        WavesMessageType.Warning));

                    return;
                }

                DrawingContext = skiaContext as ISkiaDrawingContextImpl;

                if (DrawingContext == null)
                {
                    OnMessageReceived(new WavesMessage(
                        "Initialization",
                        "Skia drawing context was not initialized.",
                        Name,
                        WavesMessageType.Warning));

                    return;
                }
            }
            catch (Exception e)
            {
                var message = new WavesMessage(
                    "Initialization",
                    $"Error occured while initialization {Name} ({Id})",
                    Name,
                    e,
                    false);

                OnMessageReceived(message);
            }
        }

        /// <inheritdoc />
        public override void Render(DrawingContext context)
        {
            try
            {
                context.DrawImage(
                    RenderTarget,
                    new Rect(0, 0, RenderTarget.PixelSize.Width, RenderTarget.PixelSize.Height),
                    new Rect(0, 0, Width, Height),
                    BitmapInterpolationMode.MediumQuality
                );

                var stream = new MemoryStream();
                RenderTarget.Save(stream);
                var bytes = stream.ToArray();
                stream.Close();
                
                Dispatcher.UIThread.Post(delegate
                {
                    try
                    {
                        using var ms = new MemoryStream(bytes);
                        var brush = new ImageBrush(new Bitmap(ms));
                        Canvas.Background = brush;
                    }
                    catch (Exception e)
                    {
                        var message = new WavesMessage(
                            "Render",
                            $"Error occured while render {Name} ({Id})",
                            Name,
                            e,
                            false);

                        OnMessageReceived(message);
                    }
                });
            }
            catch (Exception e)
            {
                var message = new WavesMessage(
                    "Render",
                    $"Error occured while render {Name} ({Id})",
                    Name,
                    e,
                    false);

                OnMessageReceived(message);
            }
        }

        /// <summary>
        ///     Notifies when message received.
        /// </summary>
        /// <param name="e">Message.</param>
        private void OnMessageReceived(IWavesMessage e)
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
        ///     Actions when redraw requested.
        /// </summary>
        private void OnRedrawRequested()
        {
            RedrawRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}