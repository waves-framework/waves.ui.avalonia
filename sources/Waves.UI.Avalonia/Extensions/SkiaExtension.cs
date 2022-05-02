// using System;
// using System.Drawing;
// using MTL.UI.Media.Drawing;
// using MTL.UI.Media.Drawing.Enums;
// using MTL.UI.Media.Drawing.Interfaces;
// using SkiaSharp;
//
// namespace MTL.UI.WPF.Drawing.Skia.Extensions
// {
//     /// <summary>
//     ///     Extensions for SKIA.
//     /// </summary>
//     public static class SkiaExtension
//     {
//         /// <summary>
//         ///     Converts system color to skia color.
//         /// </summary>
//         /// <param name="color">System color.</param>
//         /// <returns>Skia color.</returns>
//         public static SKColor ToSkColor(
//             this Color color)
//         {
//             return new SKColor(color.R, color.G, color.B, color.A);
//         }
//
//         /// <summary>
//         ///     Converts system color to skia color and set current opacity to alpha channel..
//         /// </summary>
//         /// <param name="color">System color.</param>
//         /// <param name="opacity">Opacity.</param>
//         /// <returns>Skia color.</returns>
//         public static SKColor ToSkColor(
//             this Color color,
//             double opacity)
//         {
//             var alpha = Convert.ToByte(opacity * color.A);
//             return new SKColor(color.R, color.G, color.B, alpha);
//         }
//
//         /// <summary>
//         ///     Converts MTL Point to Skia point.
//         /// </summary>
//         /// <param name="point">MTL Point.</param>
//         /// <returns>Skia point.</returns>
//         public static SKPoint ToSkPoint(
//             this MtlPoint point)
//         {
//             return new SKPoint(point.X, point.Y);
//         }
//
//         /// <summary>
//         ///     Converts MTL Text alignment to Skia text alignment.
//         /// </summary>
//         /// <param name="alignment">Mtl text alignment.</param>
//         /// <returns>Skia text alignment.</returns>
//         public static SKTextAlign ToSkTextAlign(
//             this MtlTextAlignment alignment)
//         {
//             return alignment switch
//             {
//                 MtlTextAlignment.Left => SKTextAlign.Left,
//                 MtlTextAlignment.Right => SKTextAlign.Right,
//                 MtlTextAlignment.Center => SKTextAlign.Center,
//                 _ => default
//             };
//         }
//
//         /// <summary>
//         /// Converts MTL text paint to Skia paint.
//         /// </summary>
//         /// <param name="paint">Mtl text paint.</param>
//         /// <returns>Returns skia paint.</returns>
//         public static SKPaint ToSkPaint(this IMtlTextPaint paint)
//         {
//             return new SKPaint
//             {
//                 TextSize = paint.TextStyle.FontSize,
//                 Color = paint.Fill.ToSkColor(),
//                 IsStroke = false,
//                 SubpixelText = true,
//                 IsAntialias = true,
//                 TextAlign = paint.TextStyle.Alignment.ToSkTextAlign(),
//                 Typeface = SKTypeface.FromFamilyName(paint.TextStyle.FontFamily),
//             };
//         }
//     }
// }
