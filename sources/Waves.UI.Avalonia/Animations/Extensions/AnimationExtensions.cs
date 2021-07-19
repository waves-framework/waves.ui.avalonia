using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Styling;

namespace Waves.UI.Avalonia.Animations.Extensions
{
    /// <summary>
    /// Animation extensions.
    /// </summary>
    public static class AnimationExtensions
    {
        /// <summary>
        /// Animates opacity for UI element.
        /// </summary>
        /// <param name="element">Instance of <see cref="UIElement"/>.</param>
        /// <param name="from">From value.</param>
        /// <param name="to">To value.</param>
        /// <param name="duration">Duration in milliseconds.</param>
        public static void AnimateOpacity(this Animatable element, double from, double to, int duration)
        {
            var animation = new Animation();
            animation.Children.Add(new KeyFrame() {Cue = new Cue(0), Setters = { new Setter(Visual.OpacityProperty, from)}});
            animation.Children.Add(new KeyFrame() {Cue = new Cue(duration), Setters = { new Setter(Visual.OpacityProperty, to)}});
            animation.RunAsync(element);
        }
    }
}
