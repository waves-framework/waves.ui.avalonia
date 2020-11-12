using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Threading;
using Avalonia.Xaml.Interactivity;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Drawing.ViewModel;
using MouseButton = Waves.Core.Base.Enums.MouseButton;
using Point = Waves.Core.Base.Point;

namespace Waves.UI.Avalonia.Controls.Drawing.Behavior
{
    /// <summary>
    ///     Paint behavior class.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    public class PaintBehavior<T> : Behavior<T>
        where T : Control
    {
        private object _oldDataContext;
        private double _oldHeight = -1;
        private double _oldWidth = -1;

        /// <summary>
        ///     Creates new instance of <see cref="PaintBehavior{T}" />
        /// </summary>
        public PaintBehavior(IInputService inputService)
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
        protected IInputService InputService { get; set; }

        /// <inheritdoc />
        protected override void OnAttached()
        {
            base.OnAttached();

            var element = AssociatedObject;
            if (element == null) return;

            _oldDataContext = element.DataContext;
            _oldWidth = element.Width;
            _oldHeight = element.Height;

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
        ///     Actions when property chanded.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void OnPropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            var element = AssociatedObject;
            if (element == null) return;
            
            if (e.Property.Name.Equals("Bounds"))
            {
                var bounds = element.Bounds;
                OnSizeChanged(bounds.Width, bounds.Height);
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

            AssociatedObject.InvalidateVisual();
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

            var args = new Waves.Core.Base.EventArgs.PointerEventArgs(
                MouseButton.None,
                PointerEventType.Move,
                0,
                new Point(),
                new Point((int) position.X, (int) position.Y));

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
            var args = new Waves.Core.Base.EventArgs.PointerEventArgs(
                MouseButton.None,
                PointerEventType.Enter,
                0,
                new Point(),
                new Point((int) position.X, (int) position.Y));

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
            var args = new Waves.Core.Base.EventArgs.PointerEventArgs(
                MouseButton.None,
                PointerEventType.Leave,
                0,
                new Point(),
                new Point((int) position.X, (int) position.Y));

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

            var button = MouseButton.None;

            if (e.GetCurrentPoint(element).Properties.IsLeftButtonPressed)
                button = MouseButton.Left;
            if (e.GetCurrentPoint(element).Properties.IsMiddleButtonPressed)
                button = MouseButton.Middle;
            if (e.GetCurrentPoint(element).Properties.IsRightButtonPressed)
                button = MouseButton.Right;

            var position = e.GetPosition(element);

            var args = new Waves.Core.Base.EventArgs.PointerEventArgs(
                button,
                PointerEventType.Press,
                0,
                new Point(),
                new Point((int) position.X, (int) position.Y));

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

            var button = MouseButton.None;

            if (e.GetCurrentPoint(element).Properties.IsLeftButtonPressed)
                button = MouseButton.Left;
            if (e.GetCurrentPoint(element).Properties.IsMiddleButtonPressed)
                button = MouseButton.Middle;
            if (e.GetCurrentPoint(element).Properties.IsRightButtonPressed)
                button = MouseButton.Right;

            var position = e.GetPosition(element);

            var args = new Waves.Core.Base.EventArgs.PointerEventArgs(
                button,
                PointerEventType.Release,
                0,
                new Point(),
                new Point((int) position.X, (int) position.Y));

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

            var args = new Waves.Core.Base.EventArgs.PointerEventArgs(
                MouseButton.None,
                PointerEventType.VerticalScroll,
                0,
                new Point((float) e.Delta.Y, 0),
                new Point((int) position.X, (int) position.Y));

            InputService.SetPointer(args);
        }
    }
}