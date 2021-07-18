////using System.Collections.Generic;
////using System.ComponentModel;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Tree view.
////    /// </summary>
////    public class WavesTreeView : TreeView
////    {
////        /// <summary>
////        /// Defines <see cref="CornerRadius"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> CornerRadiusProperty = AvaloniaProperty.Register<,>(
////            nameof(CornerRadius),
////            typeof(CornerRadius),
////            typeof(WavesTreeView),
////            new FrameworkPropertyMetadata(new CornerRadius(3), FrameworkPropertyMetadataOptions.AffectsRender, OnCornerRadiusChangedCallback));

////        /// <summary>
////        /// Defines <see cref="SelectedObject"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> SelectedObjectProperty = AvaloniaProperty.Register<,>(
////            nameof(SelectedObject),
////            typeof(object),
////            typeof(WavesTreeView),
////            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnSelectedObjectChangedCallback));

////        private static readonly Dictionary<DependencyObject, BindableSelectedTreeViewItemBehavior> Behaviors = new Dictionary<DependencyObject, BindableSelectedTreeViewItemBehavior>();

////        /// <summary>
////        /// Creates new instance of <see cref="WavesTreeView"/>.
////        /// </summary>
////        public WavesTreeView()
////        {
////        }

////        /// <summary>
////        /// Gets or sets corner radius.
////        /// </summary>
////        [Category("Waves.UI SDK - Appearance")]
////        public CornerRadius CornerRadius
////        {
////            get => (CornerRadius)GetValue(CornerRadiusProperty);
////            set => SetValue(CornerRadiusProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets selected item.
////        /// </summary>
////        [Category("Waves.UI SDK - Content")]
////        public object SelectedObject
////        {
////            get => GetValue(SelectedObjectProperty);
////            set => SetValue(SelectedObjectProperty, value);
////        }

////        /// <inheritdoc />
////        protected override void OnSelectedItemChanged(
////            RoutedPropertyChangedEventArgs<object> e)
////        {
////            base.OnSelectedItemChanged(e);
////            SetValue(SelectedObjectProperty, e.NewValue);
////        }

////        /// <summary>
////        /// Callback when corner radius changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnCornerRadiusChangedCallback(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (d is not TreeView treeView)
////            {
////                return;
////            }

////            treeView.SetValue(ControlHelper.CornerRadiusProperty, e.NewValue);
////        }

////        /// <summary>
////        /// Callback when selected object changed.
////        /// </summary>
////        /// <param name="d">Dependency object.</param>
////        /// <param name="e">Arguments.</param>
////        private static void OnSelectedObjectChangedCallback(
////            DependencyObject d,
////            DependencyPropertyChangedEventArgs e)
////        {
////            if (d is not TreeView treeView)
////            {
////                return;
////            }

////            if (!Behaviors.ContainsKey(treeView))
////            {
////                Behaviors.Add(treeView, new BindableSelectedTreeViewItemBehavior(treeView));
////            }

////            var view = Behaviors[treeView];
////            view.ChangeSelectedItem(e.NewValue);
////        }
////    }
////}
