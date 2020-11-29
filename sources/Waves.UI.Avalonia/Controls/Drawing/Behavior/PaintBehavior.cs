using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Threading;
using Avalonia.Xaml.Interactivity;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Drawing.ViewModel;

namespace Waves.UI.Avalonia.Controls.Drawing.Behavior
{
    /// <summary>
    ///     Paint behavior class.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    public class PaintBehavior<T> : Behavior<T>
        where T : UserControl
    {
        private object _oldDataContext;

        /// <summary>
        ///     Creates new instance of <see cref="PaintBehavior{T}" />
        /// </summary>
        protected PaintBehavior(IInputService inputService)
        {
            InputService = inputService;
        }

        /// <summary>
        ///     Gets or sets data context.
        /// </summary>
        protected DrawingElementPresenterViewModel DataContext { get; set; }

        /// <summary>
        ///     Gets or sets input service.
        /// </summary>
        private IInputService InputService { get; set; }

        /// <inheritdoc />
        protected override void OnAttached()
        {
            base.OnAttached();

            var element = AssociatedObject;
            if (element == null) return;

            _oldDataContext = element.DataContext;

            element.DataContextChanged += OnDataContextChanged;
            element.PropertyChanged += OnPropertyChanged;
            element.PointerMoved += OnMouseMove;
            element.PointerEnter += OnMouseEnter;
            element.PointerLeave += OnMouseLeave;
            element.PointerPressed += OnMouseDown;
            element.PointerReleased += OnMouseUp;
            element.PointerWheelChanged += OnMouseWheel;

            // TODO: touch.
        }

        /// <inheritdoc />
        protected override void OnDetaching()
        {
            base.OnDetaching();

            var element = AssociatedObject;
            if (element == null) return;

            element.DataContextChanged -= OnDataContextChanged;
            element.PropertyChanged -= OnPropertyChanged;
            element.PointerMoved -= OnMouseMove;
            element.PointerEnter -= OnMouseEnter;
            element.PointerLeave -= OnMouseLeave;
            element.PointerPressed -= OnMouseDown;
            element.PointerReleased -= OnMouseUp;
            element.PointerWheelChanged -= OnMouseWheel;

            if (DataContext != null)
                DataContext.RedrawRequested -= OnRedrawRequested;
        }

        /// <summary>
        ///     Actions when property changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void OnPropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            var element = AssociatedObject;
            if (element == null) return;
            
            if (e.Property.Name.Equals("Bounds"))
            {
                OnSizeChanged(element.Bounds.Width, element.Bounds.Height);
            }
        }

        /// <summary>
        ///     Actions when size changed.
        /// </summary>
        /// <param name="newWidth">New value of Width.</param>
        /// <param name="newHeight">New value of Height.</param>
        protected virtual void OnSizeChanged(double newWidth, double newHeight)
        {
            if (AssociatedObject == null) return;

            AssociatedObject.InvalidateMeasure();
            AssociatedObject.InvalidateArrange();

            if (DataContext == null) return;

            DataContext.Width = Convert.ToSingle(newWidth);
            DataContext.Height = Convert.ToSingle(newHeight);

            if (Math.Abs(newWidth) < double.Epsilon
                || Math.Abs(newHeight) < double.Epsilon)
                DataContext.IsDrawingInitialized = false;
            else
                DataContext.IsDrawingInitialized = true;
        }

        /// <summary>
        ///     Actions when data context changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnDataContextChanged(object sender, EventArgs e)
        {
            if (!(sender is T element)) return;
            if (!(element.DataContext is DrawingElementPresenterViewModel dataContext)) return;

            if (_oldDataContext is DrawingElementPresenterViewModel oldContext)
                oldContext.RedrawRequested -= OnRedrawRequested;

            DataContext = dataContext;

            // attaches input service.
            dataContext.InputService = InputService;

            DataContext.RedrawRequested += OnRedrawRequested;
        }

        /// <summary>
        ///     Notifies when redraw requested.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments/</param>
        protected virtual void OnRedrawRequested(object sender, EventArgs e)
        {
            Dispatcher.UIThread?.Post(delegate
            {
                try
                {
                    AssociatedObject?.InvalidateVisual();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            });
        }

        /// <summary>
        ///     Actions when mouse move.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnMouseMove(object sender, PointerEventArgs e)
        {
            if (InputService == null) return;
            if (!(sender is T element)) return;

            var position = e.GetPosition(element);

            var args = new Waves.Core.Base.EventArgs.WavesPointerEventArgs(
                WavesMouseButton.None,
                WavesPointerEventType.Move,
                0,
                new WavesPoint(),
                new WavesPoint((int) position.X, (int) position.Y));

            InputService.SetPointer(args);
        }

        /// <summary>
        ///     Actions when mouse enters.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnMouseEnter(object sender, PointerEventArgs e)
        {
            if (InputService == null) return;
            if (!(sender is T element)) return;

            var position = e.GetPosition(element);
            var args = new Waves.Core.Base.EventArgs.WavesPointerEventArgs(
                WavesMouseButton.None,
                WavesPointerEventType.Enter,
                0,
                new WavesPoint(),
                new WavesPoint((int) position.X, (int) position.Y));

            InputService.SetPointer(args);
        }

        /// <summary>
        ///     Actions when mouse leaves.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnMouseLeave(object sender, PointerEventArgs e)
        {
            if (InputService == null) return;
            if (!(sender is T element)) return;

            var position = e.GetPosition(element);
            var args = new Waves.Core.Base.EventArgs.WavesPointerEventArgs(
                WavesMouseButton.None,
                WavesPointerEventType.Leave,
                0,
                new WavesPoint(),
                new WavesPoint((int) position.X, (int) position.Y));

            InputService.SetPointer(args);
        }

        /// <summary>
        ///     Actions when mouse button is down.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnMouseDown(object sender, PointerPressedEventArgs e)
        {
            if (InputService == null) return;
            if (!(sender is T element)) return;

            var button = WavesMouseButton.None;

            if (e.GetCurrentPoint(element).Properties.IsLeftButtonPressed)
                button = WavesMouseButton.Left;
            if (e.GetCurrentPoint(element).Properties.IsMiddleButtonPressed)
                button = WavesMouseButton.Middle;
            if (e.GetCurrentPoint(element).Properties.IsRightButtonPressed)
                button = WavesMouseButton.Right;

            var position = e.GetPosition(element);

            var args = new Waves.Core.Base.EventArgs.WavesPointerEventArgs(
                button,
                WavesPointerEventType.Press,
                0,
                new WavesPoint(),
                new WavesPoint((int) position.X, (int) position.Y));

            InputService.SetPointer(args);
        }

        /// <summary>
        ///     Actions when mouse ups.
        ///     TODO: combine methods.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnMouseUp(object sender, PointerReleasedEventArgs e)
        {
            if (InputService == null) return;
            if (!(sender is T element)) return;

            var button = WavesMouseButton.None;

            if (e.GetCurrentPoint(element).Properties.IsLeftButtonPressed)
                button = WavesMouseButton.Left;
            if (e.GetCurrentPoint(element).Properties.IsMiddleButtonPressed)
                button = WavesMouseButton.Middle;
            if (e.GetCurrentPoint(element).Properties.IsRightButtonPressed)
                button = WavesMouseButton.Right;

            var position = e.GetPosition(element);

            var args = new Waves.Core.Base.EventArgs.WavesPointerEventArgs(
                button,
                WavesPointerEventType.Release,
                0,
                new WavesPoint(),
                new WavesPoint((int) position.X, (int) position.Y));

            InputService.SetPointer(args);
        }

        /// <summary>
        ///     Actions when mouse scrolling.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnMouseWheel(object? sender, PointerWheelEventArgs e)
        {
            if (InputService == null) return;
            if (!(sender is T element)) return;

            var position = e.GetPosition(element);

            var args = new Waves.Core.Base.EventArgs.WavesPointerEventArgs(
                WavesMouseButton.None,
                WavesPointerEventType.VerticalScroll,
                0,
                new WavesPoint((float) e.Delta.Y, 0),
                new WavesPoint((int) position.X, (int) position.Y));

            InputService.SetPointer(args);
        }
    }
}