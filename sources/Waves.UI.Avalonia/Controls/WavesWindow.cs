using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using ReactiveUI;
using Waves.Core.Base.Interfaces;
using Waves.Core.Plugins.Services.EventArgs;
using Waves.UI.Avalonia.Extensions;
using Waves.UI.Avalonia.Helpers;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Interfaces;

namespace Waves.UI.Avalonia.Controls
{
    /// <summary>
    ///     Window abstraction.
    /// </summary>
    public abstract class WavesWindow : Window,
        IWavesView, IStyleable
    {
        /// <summary>
        ///     Defines <see cref="FrontLayerContent" /> dependency property.
        /// </summary>
        public static readonly StyledProperty<StyledElement> FrontLayerContentProperty =
            AvaloniaProperty.Register<WavesWindow, StyledElement>(
                nameof(FrontLayerContent));

        /// <summary>
        ///     Defines <see cref="CanGoBack" /> dependency property.
        /// </summary>
        public static readonly StyledProperty<bool> CanGoBackProperty =
            AvaloniaProperty.Register<WavesWindow, bool>(
                nameof(CanGoBack));

        /// <summary>
        ///     Defines <see cref="GoBackCommand" /> dependency property.
        /// </summary>
        public static readonly StyledProperty<ICommand> GoBackCommandProperty =
            AvaloniaProperty.Register<WavesWindow, ICommand>(
                nameof(GoBackCommand));

        private List<IDisposable> _disposables;
        private Dictionary<string, WavesContentControl> _regionContentControls;

        /// <summary>
        ///     Creates new instance of <see cref="WavesWindow" />.
        /// </summary>
        protected WavesWindow()
        {
        }

        /// <summary>
        ///     Creates new instance of <see cref="WavesWindow" />.
        /// </summary>
        /// <param name="core">Core.</param>
        /// <param name="navigationService">Instance of navigation service.</param>
        protected WavesWindow(
            IWavesCore core,
            IWavesNavigationService navigationService)
        {
            NavigationService = navigationService;
            Core = core;
        }

        /// <inheritdoc />
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        ///     Gets or sets front layer content.
        /// </summary>
        [Category("Waves.UI SDK - Content")]
        public StyledElement FrontLayerContent
        {
            get => GetValue(FrontLayerContentProperty);
            set => SetValue(FrontLayerContentProperty, value);
        }

        /// <summary>
        ///     Gets or sets front layer content.
        /// </summary>
        [Category("Waves.UI SDK - Navigation")]
        public bool CanGoBack
        {
            get => GetValue(CanGoBackProperty);
            set => SetValue(CanGoBackProperty, value);
        }

        /// <summary>
        ///     Gets or sets front layer content.
        /// </summary>
        [Category("Waves.UI SDK - Navigation")]
        public ICommand GoBackCommand
        {
            get => GetValue(GoBackCommandProperty);
            set => SetValue(GoBackCommandProperty, value);
        }

        /// <summary>
        ///     Gets navigation service.
        /// </summary>
        protected IWavesNavigationService NavigationService { get; }

        /// <summary>
        ///     Gets core.
        /// </summary>
        protected IWavesCore Core { get; }

        /// <inheritdoc />
        Type IStyleable.StyleKey => typeof(Window);

        /// <inheritdoc />
        public virtual void RaisePropertyChanging(
            PropertyChangingEventArgs args)
        {
            OnPropertyChanging(args);
        }

        /// <inheritdoc />
        public virtual void RaisePropertyChanged(
            PropertyChangedEventArgs args)
        {
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public virtual Task InitializeAsync()
        {
            _disposables = new List<IDisposable>();

            GoBackCommand = ReactiveCommand.CreateFromTask(OnGoBack);

            _disposables.Add(FrontLayerContentProperty.Changed.Subscribe(x =>
                OnFrontLayerContentChangedCallback(x.Sender, x.NewValue.GetValueOrDefault<StyledElement>())));
            _disposables.Add(CanGoBackProperty.Changed.Subscribe(x =>
                OnCanGoBackChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>())));
            _disposables.Add(GoBackCommandProperty.Changed.Subscribe(x =>
                OnGoBackCommandChanged(x.Sender, x.NewValue.GetValueOrDefault<ICommand>())));

            this.AddResource(Constants.GenericDictionaryUri);

            SubscribeEvents();

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override async void OnInitialized()
        {
            base.OnInitialized();
            await InitializeAsync();
        }

        /// <summary>
        ///     Disposes object.
        /// </summary>
        /// <param name="disposing">
        ///     Set
        ///     <value>true</value>
        ///     if you need to release managed and unmanaged resources. Set
        ///     <value>false</value>
        ///     if need to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(
            bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            UnsubscribeEvents();

            if (_regionContentControls == null)
            {
                return;
            }

            foreach (var control in _regionContentControls)
            {
                NavigationService.UnregisterContentControl(control.Key);
            }
        }

        /// <summary>
        ///     Callback when property changing.
        /// </summary>
        /// <param name="e">Arguments.</param>
        protected virtual void OnPropertyChanging(
            PropertyChangingEventArgs e)
        {
            PropertyChanging?.Invoke(this, e);
        }

        /// <summary>
        ///     Callback when front layer content changed.
        /// </summary>
        /// <param name="d">Dependency object.</param>
        /// <param name="e">Arguments.</param>
        private static void OnFrontLayerContentChangedCallback(
            IAvaloniaObject d,
            object e)
        {
            if (d is not WavesWindow window)
            {
                return;
            }

            window.SetValue(WindowHelper.FrontLayerContentProperty, e);
        }

        /// <summary>
        ///     Callback when <see cref="CanGoBackProperty" /> changed.
        /// </summary>
        /// <param name="d">Dependency object.</param>
        /// <param name="e">Arguments.</param>
        private static void OnCanGoBackChanged(
            IAvaloniaObject d,
            object e)
        {
            if (d is not WavesWindow window)
            {
                return;
            }

            window.SetValue(WindowHelper.CanGoBackProperty, e);
        }

        /// <summary>
        ///     Callback when <see cref="GoBackCommandProperty" /> changed.
        /// </summary>
        /// <param name="d">Dependency object.</param>
        /// <param name="e">Arguments.</param>
        private static void OnGoBackCommandChanged(
            IAvaloniaObject d,
            object e)
        {
            if (d is not WavesWindow window)
            {
                return;
            }

            window.SetValue(WindowHelper.GoBackCommandProperty, e);
        }

        /// <summary>
        ///     Actions when need to go back.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        private async Task OnGoBack()
        {
            if (Content is not StyledElement element)
            {
                return;
            }

            if (element.DataContext is not IWavesViewModel viewModel)
            {
                return;
            }

            await NavigationService.GoBackAsync(viewModel);
        }

        /// <summary>
        ///     Notifies from navigation service that go back changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void NavigationServiceOnGoBackChanged(
            object sender,
            GoBackNavigationEventArgs e)
        {
            if (!e.ContentControl.Equals(this))
            {
                return;
            }

            CanGoBack = e.CanGoBack;
        }

        /// <summary>
        ///     Subscribes to events.
        /// </summary>
        private void SubscribeEvents()
        {
            NavigationService.GoBackChanged += NavigationServiceOnGoBackChanged;
        }

        /// <inheritdoc />
        protected override void OnOpened(
            EventArgs e)
        {
            base.OnOpened(e);

            //// TODO: initialization.
            _regionContentControls = this.FindRegions(NavigationService);
            this.InitializeControl(Core);
            ////this.InitializeSurfaces(Core);
        }

        /// <summary>
        ///     Unsubscribes from events.
        /// </summary>
        private void UnsubscribeEvents()
        {
            NavigationService.GoBackChanged -= NavigationServiceOnGoBackChanged;

            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}