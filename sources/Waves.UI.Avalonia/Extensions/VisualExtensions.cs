using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Interfaces;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Interfaces;

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
        {
            var content = control;
            if (content is ContentControl contentControl)
            {
                content = contentControl.Content as IVisual;
            }

            if (content == null)
            {
                yield break;
            }
            
            foreach (var child in content.VisualChildren)
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

        /// <summary>
        ///     Finds all waves tab controls.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="core">Core.</param>
        /// <returns>List of initialized controls.</returns>
        public static List<IWavesControl> InitializeControl(this IVisual obj, IWavesCore core)
        {
            var controls = obj.FindVisualChildren<IWavesControl>();
            var result = new List<IWavesControl>();

            foreach (var control in controls)
            {
                try
                {
                    control.AttachCore(core);
                    result.Add(control);
                }
                catch (Exception e)
                {
                    core.WriteLogAsync(e, core);
                }
            }

            if (result.Count > 0)
            {
                var controlsText = result.Count == 1 ? "control" : "controls";
                
                core.WriteLogAsync(
                    "Attaching core",
                    $"Core attached to {result.Count} {controlsText}.",
                    obj as IWavesView,
                    WavesMessageType.Information);
            }
            
            return result;
        }
    }
}