using System;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia;
using Avalonia.Media;

namespace Waves.UI.Avalonia.Controls.Primitives
{
    /// <summary>
    /// Arc control.
    /// </summary>
    public sealed class Arc : StyledElement
    {
        /// <summary>
        /// Defines start angle property.
        /// </summary>
        public static readonly StyledProperty<double> StartAngleProperty = AvaloniaProperty.Register<Arc, double>(
            "StartAngle",
            0.0d);

        /// <summary>
        /// Defines sweep angle property.
        /// </summary>
        public static readonly StyledProperty<double> SweepAngleProperty = AvaloniaProperty.Register<Arc, double>(
            "SweepAngle",
            180d);

        /// <summary>
        /// Defines radius property.
        /// </summary>
        public static readonly StyledProperty<double> RadiusProperty = AvaloniaProperty.Register<Arc, double>(
            "Radius",
            10.0d);

        /// <summary>
        /// Defines stroke brush property.
        /// </summary>
        public static readonly StyledProperty<Brush> StrokeProperty = AvaloniaProperty.Register<Arc, Brush>(
            "Stroke");

        /// <summary>
        /// Defines stroke thickness property.
        /// </summary>
        public static readonly StyledProperty<double> StrokeThicknessProperty = AvaloniaProperty.Register<Arc, double>(
            "StrokeThickness",
            1d);

        /// <summary>
        /// Defines center point location.
        /// </summary>
        public static readonly StyledProperty<Point> CenterProperty = AvaloniaProperty.Register<Arc, Point>(
            "Center");

        /// <summary>
        /// Defines whether override center point is needed.
        /// </summary>
        public static readonly StyledProperty<bool> OverrideCenterProperty = AvaloniaProperty.Register<Arc, bool>(
            "OverrideCenter",
            false);

        /// <summary>
        /// Gets or sets center.
        /// </summary>
        [Category("Arc")]
        public Point Center
        {
            get => (Point)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        /// <summary>
        /// Gets or sets whether override center is needed.
        /// </summary>
        [Category("Arc")]
        public bool OverrideCenter
        {
            get => (bool)GetValue(OverrideCenterProperty);
            set => SetValue(OverrideCenterProperty, value);
        }

        /// <summary>
        /// Gets or sets start angle.
        /// </summary>
        [Category("Arc")]
        public double StartAngle
        {
            get => (double)GetValue(StartAngleProperty);
            set => SetValue(StartAngleProperty, value);
        }

        /// <summary>
        /// Gets or sets sweep angle.
        /// </summary>
        [Category("Arc")]
        public double SweepAngle
        {
            get => (double)GetValue(SweepAngleProperty);
            set => SetValue(SweepAngleProperty, value);
        }

        /// <summary>
        /// Gets or sets radius.
        /// </summary>
        [Category("Arc")]
        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        /// <summary>
        /// Gets or sets stroke thickness.
        /// </summary>
        [Category("Arc")]
        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        /// <summary>
        /// Gets or sets stroke brush.
        /// </summary>
        [Category("Arc")]
        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        /// <summary>
        /// Converts polar coordinate to cartesian.
        /// </summary>
        /// <param name="angle">Angle.</param>
        /// <param name="radius">Radius.</param>
        /// <param name="center">Center.</param>
        /// <returns>Return cartesian coordinate.</returns>
        public static Point PolarToCartesian(
            double angle,
            double radius,
            Point center)
        {
            var angleRad = Math.PI / 180.0 * (angle - 90);

            var x = radius * Math.Cos(angleRad) + center.X;
            var y = radius * Math.Sin(angleRad) + center.Y;

            return new Point(x, y);
        }

        /////// <inheritdoc />
        ////protected override void OnRender(
        ////    DrawingContext dc)
        ////{
        ////    base.OnRender(dc);
        ////    Draw(dc);
        ////}

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

        /////// <summary>
        /////// Draws arc.
        /////// </summary>
        /////// <param name="dc">Drawing context.</param>
        ////private void Draw(
        ////    DrawingContext dc)
        ////{
        ////    var center = OverrideCenter ? CenterPointFromRect(new Rect(RenderSize)) : Center;

        ////    var startPoint = PolarToCartesian(StartAngle, Radius, center);
        ////    var endPoint = PolarToCartesian(StartAngle + SweepAngle, Radius, center);

        ////    var isLarge = StartAngle + SweepAngle - StartAngle > 180;

        ////    var segments = new List<PathSegment>(1)
        ////    {
        ////        new ArcSegment(endPoint, new Size(Radius, Radius), 0.0, isLarge, SweepDirection.Clockwise, true),
        ////    };

        ////    var figures = new List<PathFigure>(1);
        ////    var pf = new PathFigure(startPoint, segments, true) { IsClosed = false };

        ////    figures.Add(pf);

        ////    Geometry g = new PathGeometry(figures, FillRule.EvenOdd, null);

        ////    dc.DrawGeometry(null, new Pen(Stroke, StrokeThickness), g);
        ////}
    }
}
