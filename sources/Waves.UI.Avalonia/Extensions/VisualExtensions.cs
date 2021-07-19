using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Plugins.Services.Interfaces;

namespace Waves.UI.Avalonia.Extensions
{
    /// <summary>
    /// Control extensions.
    /// </summary>
    public static class VisualExtensions
    {
        /// <summary>
        ///     Finds all controls by current type.
        /// </summary>
        /// <typeparam name="T">Current type.</typeparam>
        /// <param name="control">Control.</param>
        /// <returns>Collection of controls.</returns>
        public static IEnumerable<T> FindVisualChildren<T>(
            this IVisual control)
            where T : IControl
        {
            foreach (var child in control.GetVisualChildren())
            {
                if (child is T childType)
                {
                    yield return childType;
                }

                foreach (var other in FindVisualChildren<T>(child))
                {
                    yield return other;
                }
            }
        }

        /// <summary>
        ///     Finds all waves content control with regions in object.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="navigationService">Navigation service.</param>
        /// <returns>Regions / Control dictionary.</returns>
        public static Dictionary<string, WavesContentControl> FindRegions(this IVisual obj, IWavesNavigationService navigationService)
        {
            var controls = obj.FindVisualChildren<WavesContentControl>();

            var dictionary = new Dictionary<string, WavesContentControl>();

            foreach (var control in controls)
            {
                var region = control.GetValue(WavesContentControl.RegionProperty);

                if (region == null)
                {
                    continue;
                }

                dictionary.Add((string)region, control);

                navigationService.RegisterContentControl(region, control);
            }

            return dictionary;
        }

        /////// <summary>
        ///////     Finds all waves tab controls.
        /////// </summary>
        /////// <param name="obj">Object.</param>
        /////// <param name="core">Core.</param>
        /////// <returns>List of initialized controls.</returns>
        ////public static List<MtlTabControl> InitializeTabControls(this IVisual obj, IMtlCore core)
        ////{
        ////    var controls = obj.FindVisualChildren<MtlTabControl>();
        ////    var result = new List<MtlTabControl>();

        ////    foreach (var control in controls)
        ////    {
        ////        try
        ////        {
        ////            control.InitializeSelector(core);
        ////            result.Add(control);
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            core.WriteLogAsync(e, core);
        ////        }
        ////    }

        ////    return result;
        ////}

        /////// <summary>
        ///////     Finds all MTL mtl surfaces.
        /////// </summary>
        /////// <param name="obj">Object.</param>
        /////// <param name="core">Core.</param>
        /////// <returns>List of initialized controls.</returns>
        ////public static List<MtlSurface> InitializeSurfaces(this DependencyObject obj, IMtlCore core)
        ////{
        ////    var controls = obj.FindVisualChildren<MtlSurface>();
        ////    var result = new List<MtlSurface>();

        ////    foreach (var control in controls)
        ////    {
        ////        try
        ////        {
        ////            control.InitializeElement(core);
        ////            result.Add(control);
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            core.WriteLogAsync(e, core);
        ////        }
        ////    }

        ////    return result;
        ////}
    }
}