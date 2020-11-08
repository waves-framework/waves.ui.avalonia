using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using Waves.Core.Base;
using Waves.UI.Avalonia.Extensions;
using Waves.UI.Drawing.Base.Interfaces;
using Object = Waves.Core.Base.Object;
using Point = Waves.Core.Base.Point;
using Size = Waves.Core.Base.Size;

namespace Waves.UI.Avalonia.Controls.Drawing.Engines.Avalonia.View
{
    /// <summary>
    ///     Drawing element.
    /// </summary>
    public class AvaloniaDrawingElement : Object, IDrawingElement
    {
        /// <summary>
        ///     Gets or sets drawing canvas.
        /// </summary>
        private Canvas Canvas { get; set; }
        
        /// <inheritdoc />
        public override Guid Id { get; } = Guid.Parse("D10DB82B-0EEA-4E95-AFED-4F3B856DF89B");
        
        /// <inheritdoc />
        public override string Name { get; set; } = "Avalonia Drawing Element";

        /// <inheritdoc />
        public void Draw(object element, ICollection<IDrawingObject> drawingObjects)
        {
            if (!(element is Canvas canvas)) return;

            try
            {
                Canvas ??= canvas;
                Canvas.Children.Clear();

                foreach (var obj in drawingObjects)
                    obj.Draw(this);
            }
            catch (Exception e)
            {
                OnMessageReceived(
                    this,
                    new Message(
                        "Drawing", 
                        "Error occurred while drawing.", 
                        Name, 
                        e, 
                        false));
            }
        }

        /// <inheritdoc />
        public void DrawEllipse(Point location, float radius, IPaint paint)
        {
            try
            {
                var x = location.X - radius;
                var y = location.Y - radius;

                Canvas.Children.Add(new Ellipse
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(x, y, 0, 0),
                    Width = radius * 2,
                    Height = radius * 2,
                    Fill = new SolidColorBrush(paint.Fill.ToSystemColor()),
                    Stroke = new SolidColorBrush(paint.Stroke.ToSystemColor()),
                    StrokeThickness = paint.StrokeThickness,
                    Opacity = paint.Opacity
                });
            }
            catch (Exception e)
            {
                OnMessageReceived(
                    this,
                    new Message(
                        "Drawing", 
                        "Error occurred while drawing ellipse.", 
                        Name, 
                        e, 
                        false));
            }
        }

        /// <inheritdoc />
        public void DrawLine(Point point1, Point point2, IPaint paint)
        {
            try
            {
                var dashPattern = new AvaloniaList<double>();

                if (paint.DashPattern != null && paint.DashPattern.Length == 4)
                {
                    dashPattern = new AvaloniaList<double>()
                    {
                        paint.DashPattern[0],
                        paint.DashPattern[1],
                        paint.DashPattern[2],
                        paint.DashPattern[3],
                    };
                }

                Canvas.Children.Add(new Line
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    StartPoint = new global::Avalonia.Point(point1.X, point1.Y),
                    EndPoint = new global::Avalonia.Point(point2.X, point2.Y),
                    Fill = new SolidColorBrush(paint.Fill.ToSystemColor()),
                    Stroke = new SolidColorBrush(paint.Stroke.ToSystemColor()),
                    StrokeThickness = paint.StrokeThickness,
                    StrokeDashArray = dashPattern,
                    Opacity = paint.Opacity
                });
            }
            catch (Exception e)
            {
                OnMessageReceived(
                    this,
                    new Message(
                        "Drawing", 
                        "Error occurred while drawing line.", 
                        Name, 
                        e, 
                        false));
            }
        }

        /// <inheritdoc />
        public void DrawRectangle(Point location, Size size, IPaint paint, float cornerRadius = 0)
        {
            if (size.Width < 0 || size.Height < 0) return;

            try
            {
                Canvas.Children.Add(new Border
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(location.X, location.Y, 0, 0),
                    Width = size.Width,
                    Height = size.Height,
                    Background = new SolidColorBrush(paint.Fill.ToSystemColor()),
                    BorderBrush = new SolidColorBrush(paint.Stroke.ToSystemColor()),
                    BorderThickness = new Thickness(paint.StrokeThickness),
                    CornerRadius = new CornerRadius(cornerRadius),
                    Opacity = paint.Opacity
                });
            }
            catch (Exception e)
            {
                OnMessageReceived(
                    this,
                    new Message(
                        "Drawing", 
                        "Error occurred while drawing rectangle.", 
                        Name, 
                        e, 
                        false));
            }
        }

        /// <inheritdoc />
        public void DrawText(Point location, string text, ITextPaint paint)
        {
            try
            {
                var textBlock = new TextBlock
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Text = text,
                    Foreground = new SolidColorBrush(paint.Fill.ToSystemColor()),
                    FontSize = paint.TextStyle.FontSize,
                    FontFamily = new FontFamily(paint.TextStyle.FontFamily),
                    Margin = new Thickness(location.X, location.Y, 0, 0),
                    TextAlignment = paint.TextStyle.Alignment.ToSystemTextAlignment(),
                    Opacity = paint.Opacity
                };

                textBlock.Measure(new global::Avalonia.Size(double.PositiveInfinity, double.PositiveInfinity));
                textBlock.Arrange(new Rect(0, 0, textBlock.DesiredSize.Width, textBlock.DesiredSize.Height));

                textBlock.Margin = new Thickness(location.X, location.Y - textBlock.Width, 0, 0);

                Canvas.Children.Add(textBlock);
            }
            catch (Exception e)
            {
                OnMessageReceived(
                    this,
                    new Message(
                        "Drawing", 
                        "Error occurred while drawing text.", 
                        Name, 
                        e, 
                        false));
            }
        }

        /// <inheritdoc />
        public Size MeasureText(string text, ITextPaint paint)
        {
            var textBlock = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Text = text,
                Foreground = new SolidColorBrush(paint.Fill.ToSystemColor()),
                FontSize = paint.TextStyle.FontSize,
                FontFamily = new FontFamily(paint.TextStyle.FontFamily),
                TextAlignment = paint.TextStyle.Alignment.ToSystemTextAlignment(),
                Opacity = paint.Opacity
            };

            // TODO
            return new Size(0, 0);

            textBlock.Measure(new global::Avalonia.Size(double.PositiveInfinity, double.PositiveInfinity));
            textBlock.Arrange(new Rect(0, 0, textBlock.DesiredSize.Width, textBlock.DesiredSize.Height));

            return new Size(Convert.ToSingle(textBlock.Width), Convert.ToSingle(textBlock.Height));
        }
        
        /// <inheritdoc />
        public override void Dispose()
        {
        }
    }
}