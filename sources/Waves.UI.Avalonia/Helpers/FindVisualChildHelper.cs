// namespace Waves.UI.Avalonia.Helpers
// {
//     /// <summary>
//     /// Helper class for visual child search.
//     /// </summary>
//     public static class FindVisualChildHelper
//     {
//         /// <summary>
//         /// Gets first visual child of current type.
//         /// </summary>
//         /// <typeparam name="T">Type.</typeparam>
//         /// <param name="dependencyObject">Dependency object.</param>
//         /// <returns>Returns first matched visual child.</returns>
//         public static T GetFirstChildOfType<T>(DependencyObject dependencyObject)
//             where T : DependencyObject
//         {
//             if (dependencyObject == null)
//             {
//                 return null;
//             }
//
//             for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
//             {
//                 var child = VisualTreeHelper.GetChild(dependencyObject, i);
//
//                 var result = (child as T) ?? GetFirstChildOfType<T>(child);
//
//                 if (result != null)
//                 {
//                     return result;
//                 }
//             }
//
//             return null;
//         }
//     }
// }
