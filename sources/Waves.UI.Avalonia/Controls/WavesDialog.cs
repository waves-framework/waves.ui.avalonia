////using System;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Threading.Tasks;
////using System.Windows.Input;
////using Waves.UI.Plugins.Services.Interfaces;
////using Waves.UI.Presentation.Dialogs.Interfaces;
////using Waves.UI.Presentation.Interfaces;

////namespace Waves.UI.Avalonia.Controls
////{
////    /// <summary>
////    /// Dialog control.
////    /// </summary>
////    [ContentProperty("Content")]
////    [DefaultProperty("Content")]
////    public class WavesDialog : ContentControl, IWavesView
////    {
////        /// <summary>
////        /// Defines <see cref="Title"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> TitleProperty = AvaloniaProperty.Register<,>(
////            "Title",
////            typeof(string),
////            typeof(WavesDialog),
////            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

////        /// <summary>
////        /// Defines <see cref="Tools"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> ToolsProperty = AvaloniaProperty.Register<,>(
////            "Tools",
////            typeof(IEnumerable<IWavesTool>),
////            typeof(WavesDialog),
////            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

////        /// <summary>
////        /// Defines <see cref="DoneCommand"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> DoneCommandProperty = AvaloniaProperty.Register<,>(
////            "DoneCommand",
////            typeof(ICommand),
////            typeof(WavesDialog),
////            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

////        /// <summary>
////        /// Defines <see cref="DoneButtonCaption"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> DoneButtonCaptionProperty = AvaloniaProperty.Register<,>(
////            "DoneButtonCaption",
////            typeof(string),
////            typeof(WavesDialog),
////            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

////        /// <summary>
////        /// Defines <see cref="IsDoneButtonVisible"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> IsDoneButtonVisibleProperty = AvaloniaProperty.Register<,>(
////            "IsDoneButtonVisible",
////            typeof(bool),
////            typeof(WavesDialog),
////            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

////        /// <summary>
////        /// Defines <see cref="CancelCommand"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> CancelCommandProperty = AvaloniaProperty.Register<,>(
////            "CancelCommand",
////            typeof(ICommand),
////            typeof(WavesDialog),
////            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

////        /// <summary>
////        /// Defines <see cref="CancelButtonCaption"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> CancelButtonCaptionProperty = AvaloniaProperty.Register<,>(
////            "CancelButtonCaption",
////            typeof(string),
////            typeof(WavesDialog),
////            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

////        /// <summary>
////        /// Defines <see cref="IsCancelButtonVisible"/> property.
////        /// </summary>
////        public static readonly StyledProperty<> IsCancelButtonVisibleProperty = AvaloniaProperty.Register<,>(
////            "IsCancelButtonVisible",
////            typeof(bool),
////            typeof(WavesDialog),
////            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

////        private Dictionary<string, WavesContentControl> _regionContentControls;

////        /// <summary>
////        /// Creates new instance of <see cref="WavesDialog"/>.
////        /// </summary>
////        static WavesDialog()
////        {
////            DefaultStyleKeyProperty.OverrideMetadata(typeof(WavesDialog), new FrameworkPropertyMetadata(typeof(WavesDialog)));
////            HorizontalContentAlignmentProperty.OverrideMetadata(typeof(WavesDialog), new FrameworkPropertyMetadata(HorizontalAlignment.Stretch));
////            VerticalContentAlignmentProperty.OverrideMetadata(typeof(WavesDialog), new FrameworkPropertyMetadata(VerticalAlignment.Stretch));
////        }

////        /// <summary>
////        /// Creates new instance of <see cref="WavesDialog"/>.
////        /// </summary>
////        /// <param name="core">Core.</param>
////        /// <param name="navigationService">Instance of navigation service.</param>
////        protected WavesDialog(IWavesCore core, IWavesNavigationService navigationService)
////        {
////            NavigationService = navigationService;
////            Core = core;

////            Loaded += OnLoaded;
////        }

////        /// <inheritdoc />
////        public event PropertyChangedEventHandler PropertyChanged;

////        /// <inheritdoc />
////        public event PropertyChangingEventHandler PropertyChanging;

////        /// <summary>
////        /// Gets or sets dialog title.
////        /// </summary>
////        [Category("Waves.UI - Appearance")]
////        public string Title
////        {
////            get => (string)GetValue(TitleProperty);
////            set => SetValue(TitleProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets dialog additional actions.
////        /// </summary>
////        [Category("Waves.UI - Actions")]
////        public IEnumerable<IWavesTool> Tools
////        {
////            get => (IEnumerable<IWavesTool>)GetValue(ToolsProperty);
////            set => SetValue(ToolsProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets whether "Done" button is visible.
////        /// </summary>
////        [Category("Waves.UI - Actions")]
////        public bool IsDoneButtonVisible
////        {
////            get => (bool)GetValue(IsDoneButtonVisibleProperty);
////            set => SetValue(IsDoneButtonVisibleProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets "Done" command.
////        /// </summary>
////        [Category("Waves.UI - Actions")]
////        public ICommand DoneCommand
////        {
////            get => (ICommand)GetValue(DoneCommandProperty);
////            set => SetValue(DoneCommandProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets "Done" button caption.
////        /// </summary>
////        [Category("Waves.UI - Actions")]
////        public string DoneButtonCaption
////        {
////            get => (string)GetValue(DoneButtonCaptionProperty);
////            set => SetValue(DoneButtonCaptionProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets whether "Cancel" button is visible.
////        /// </summary>
////        [Category("Waves.UI - Actions")]
////        public bool IsCancelButtonVisible
////        {
////            get => (bool)GetValue(IsCancelButtonVisibleProperty);
////            set => SetValue(IsCancelButtonVisibleProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets "Cancel" command.
////        /// </summary>
////        [Category("Waves.UI - Actions")]
////        public ICommand CancelCommand
////        {
////            get => (ICommand)GetValue(CancelCommandProperty);
////            set => SetValue(CancelCommandProperty, value);
////        }

////        /// <summary>
////        /// Gets or sets "Cancel" button caption.
////        /// </summary>
////        [Category("Waves.UI - Actions")]
////        public string CancelButtonCaption
////        {
////            get => (string)GetValue(CancelButtonCaptionProperty);
////            set => SetValue(CancelButtonCaptionProperty, value);
////        }

////        /// <summary>
////        /// Gets navigation service.
////        /// </summary>
////        protected IWavesNavigationService NavigationService { get; }

////        /// <summary>
////        /// Gets core.
////        /// </summary>
////        protected IWavesCore Core { get; }

////        /// <inheritdoc />
////        public virtual void RaisePropertyChanging(PropertyChangingEventArgs args)
////        {
////            OnPropertyChanging(args);
////        }

////        /// <inheritdoc />
////        public virtual void RaisePropertyChanged(PropertyChangedEventArgs args)
////        {
////            OnPropertyChanged(args);
////        }

////        /// <inheritdoc />
////        public void Dispose()
////        {
////            Dispose(true);
////            GC.SuppressFinalize(this);
////        }

////        /// <inheritdoc />
////        public virtual Task InitializeAsync()
////        {
////            _regionContentControls = this.FindRegions(NavigationService);

////            return Task.CompletedTask;
////        }

////        /// <inheritdoc />
////        protected override async void OnInitialized(
////            EventArgs e)
////        {
////            base.OnInitialized(e);
////            await InitializeAsync();
////        }

////        /// <summary>
////        ///     Disposes object.
////        /// </summary>
////        /// <param name="disposing">Set
////        ///     <value>true</value>
////        ///     if you need to release managed and unmanaged resources. Set
////        ///     <value>false</value>
////        ///     if need to release only unmanaged resources.
////        /// </param>
////        protected virtual void Dispose(bool disposing)
////        {
////            if (!disposing)
////            {
////                return;
////            }

////            Loaded -= OnLoaded;

////            if (_regionContentControls == null)
////            {
////                return;
////            }

////            foreach (var control in _regionContentControls)
////            {
////                NavigationService.UnregisterContentControl(control.Key);
////            }
////        }

////        /// <summary>
////        /// Callback when property changed.
////        /// </summary>
////        /// <param name="e">Arguments.</param>
////        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
////        {
////            PropertyChanged?.Invoke(this, e);
////        }

////        /// <summary>
////        /// Callback when property changing.
////        /// </summary>
////        /// <param name="e">Arguments.</param>
////        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
////        {
////            PropertyChanging?.Invoke(this, e);
////        }

////        /// <summary>
////        /// Callback when loaded.
////        /// </summary>
////        /// <param name="sender">Sender.</param>
////        /// <param name="e">Arguments.</param>
////        private void OnLoaded(
////            object sender,
////            RoutedEventArgs e)
////        {
////            _regionContentControls = this.FindRegions(NavigationService);
////            this.InitializeTabControls(Core);
////            this.InitializeSurfaces(Core);
////        }
////    }
////}
