using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Waves.UI.Avalonia.Controls;

/// <summary>
/// Arc control.
/// </summary>
public sealed class WavesArc : Control
{
    /// <summary>
    /// Defines start angle property.
    /// </summary>
    public static readonly StyledProperty<double> StartAngleProperty = AvaloniaProperty.Register<WavesArc, double>(
        "StartAngle",
        0.0d);

    /// <summary>
    /// Defines sweep angle property.
    /// </summary>
    public static readonly StyledProperty<double> SweepAngleProperty = AvaloniaProperty.Register<WavesArc, double>(
        "SweepAngle",
        180d);

    /// <summary>
    /// Defines radius property.
    /// </summary>
    public static readonly StyledProperty<double> RadiusProperty = AvaloniaProperty.Register<WavesArc, double>(
        "Radius",
        10.0d);

    /// <summary>
    /// Defines stroke brush property.
    /// </summary>
    public static readonly StyledProperty<Brush> StrokeProperty = AvaloniaProperty.Register<WavesArc, Brush>(
        "Stroke");

    /// <summary>
    /// Defines stroke thickness property.
    /// </summary>
    public static readonly StyledProperty<double> StrokeThicknessProperty = AvaloniaProperty.Register<WavesArc, double>(
        "StrokeThickness",
        1d);

    /// <summary>
    /// Defines center point location.
    /// </summary>
    public static readonly StyledProperty<Point> CenterProperty = AvaloniaProperty.Register<WavesArc, Point>(
        "Center");

    /// <summary>
    /// Defines whether override center point is needed.
    /// </summary>
    public static readonly StyledProperty<bool> OverrideCenterProperty = AvaloniaProperty.Register<WavesArc, bool>(
        "OverrideCenter",
        false);

    /// <summary>
    /// Gets or sets center.
    /// </summary>
    [Category("Arc")]
    public Point Center
    {
        get => (Point)GetValue(CenterProperty);
        set
        {
            SetValue(CenterProperty, value);
            InvalidateVisual();
        }
    }

    /// <summary>
    /// Gets or sets whether override center is needed.
    /// </summary>
    [Category("Arc")]
    public bool OverrideCenter
    {
        get => (bool)GetValue(OverrideCenterProperty);
        set
        {
            SetValue(OverrideCenterProperty, value);
            InvalidateVisual();
        }
    }

    /// <summary>
    /// Gets or sets start angle.
    /// </summary>
    [Category("Arc")]
    public double StartAngle
    {
        get => (double)GetValue(StartAngleProperty);
        set
        {
            SetValue(StartAngleProperty, value);
            InvalidateVisual();
        }
    }

    /// <summary>
    /// Gets or sets sweep angle.
    /// </summary>
    [Category("Arc")]
    public double SweepAngle
    {
        get => (double)GetValue(SweepAngleProperty);
        set
        {
            SetValue(SweepAngleProperty, value);
            InvalidateVisual();
        }
    }

    /// <summary>
    /// Gets or sets radius.
    /// </summary>
    [Category("Arc")]
    public double Radius
    {
        get => (double)GetValue(RadiusProperty);
        set
        {
            SetValue(RadiusProperty, value);
            InvalidateVisual();
        }
    }

    /// <summary>
    /// Gets or sets stroke thickness.
    /// </summary>
    [Category("Arc")]
    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set
        {
            SetValue(StrokeThicknessProperty, value);
            InvalidateVisual();
        }
    }

    /// <summary>
    /// Gets or sets stroke brush.
    /// </summary>
    [Category("Arc")]
    public Brush Stroke
    {
        get => (Brush)GetValue(StrokeProperty);
        set
        {
            SetValue(StrokeProperty, value);
            InvalidateVisual();
        }
    }

    /// <inheritdoc />
    public override void Render(DrawingContext context)
    {
        Draw(context);
        base.Render(context);
    }

    /// <inheritdoc />
    public override void EndInit()
    {
        base.EndInit();

        SweepAngleProperty.Changed.Subscribe(x =>
            SweepAnglePropertyChangedCallback(x.Sender, x.NewValue.GetValueOrDefault<double>()));
    }

    /// <summary>
    /// Converts polar coordinate to cartesian.
    /// </summary>
    /// <param name="angle">Angle.</param>
    /// <param name="radius">Radius.</param>
    /// <param name="center">Center.</param>
    /// <returns>Return cartesian coordinate.</returns>
    private static Point PolarToCartesian(
        double angle,
        double radius,
        Point center)
    {
        var angleRad = Math.PI / 180.0 * (angle - 90);

        var x = radius * Math.Cos(angleRad) + center.X;
        var y = radius * Math.Sin(angleRad) + center.Y;

        return new Point(x, y);
    }

    /// <summary>
    /// Gets center point from rect.
    /// </summary>
    /// <param name="rect">Rect.</param>
    /// <returns>Returns center point.</returns>
    private static Point CenterPointFromRect(
        Rect rect)
    {
        return new Point(rect.Width / 2, rect.Height / 2);
    }

    /// <summary>
    /// Draws control.
    /// </summary>
    /// <param name="context">Drawing context.</param>
    private void Draw(DrawingContext context)
    {
        var center = OverrideCenter ? CenterPointFromRect(new Rect(DesiredSize)) : Center;
        var startPoint = PolarToCartesian(StartAngle, Radius, center);
        var endPoint = PolarToCartesian(StartAngle + SweepAngle, Radius, center);
        var isLarge = StartAngle + SweepAngle - StartAngle > 180;
        var segments = new PathSegments()
        {
            new ArcSegment()
            {
                IsLargeArc = isLarge,
                Point = endPoint,
                Size = new Size(Radius, Radius),
                RotationAngle = 0.0,
                SweepDirection = SweepDirection.Clockwise,
            },
        };
        var figures = new PathFigures();
        var pf = new PathFigure()
        {
            StartPoint = startPoint,
            Segments = segments,
            IsFilled = true,
            IsClosed = false,
        };
        figures.Add(pf);
        Geometry g = new PathGeometry()
        {
            Figures = figures,
            FillRule = FillRule.EvenOdd,
            Transform = null,
        };

        context.DrawGeometry(new SolidColorBrush(), new Pen(Stroke, StrokeThickness), g);
    }

    /// <summary>
    /// Callback when <see cref="SweepAngleProperty"/> changed.
    /// </summary>
    /// <param name="d">Sender.</param>
    /// <param name="e">Value.</param>
    private void SweepAnglePropertyChangedCallback(IAvaloniaObject d, double e)
    {
        if (d is not WavesArc arc)
        {
            return;
        }

        // TODO: other properties
        arc.InvalidateVisual();
    }
}
