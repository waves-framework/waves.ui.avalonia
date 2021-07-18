////using System;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Content control for template generator.
////    /// </summary>
////    public sealed class WavesTemplateGeneratorControl : ContentControl
////    {
////        /// <summary>
////        /// Defines control factory property.
////        /// </summary>
////        internal static readonly StyledProperty<> FactoryProperty = AvaloniaProperty.Register<,>("Factory", typeof(Func<object>), typeof(WavesTemplateGeneratorControl), new PropertyMetadata(null, OnFactoryChanged));

////        /// <summary>
////        /// Actions when factory changed.
////        /// </summary>
////        /// <param name="instance">Instance.</param>
////        /// <param name="args">Arguments.</param>
////        private static void OnFactoryChanged(DependencyObject instance, DependencyPropertyChangedEventArgs args)
////        {
////            var control = (WavesTemplateGeneratorControl)instance;
////            var factory = (Func<object>)args.NewValue;

////            if (factory == null)
////            {
////                return;
////            }

////            control.Content = factory();
////        }
////    }
////}
