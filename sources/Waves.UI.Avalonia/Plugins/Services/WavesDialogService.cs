using System;
using System.Threading.Tasks;
using Waves.Core.Base;
using Waves.Core.Base.Attributes;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Dialogs;
using Waves.UI.Presentation.Dialogs.Enums;
using Waves.UI.Presentation.Dialogs.Parameters;

namespace Waves.UI.Avalonia.Plugins.Services
{
    /// <summary>
    ///     WPF dialog service implementation.
    /// </summary>
    [WavesService(typeof(IWavesDialogService))]
    public class WavesDialogService : WavesService,
        IWavesDialogService
    {
        private readonly IWavesNavigationService _navigationService;

        /// <summary>
        ///     Creates new instance of <see cref="WavesDialogService" />.
        /// </summary>
        /// <param name="navigationService">Instance of navigation service.</param>
        public WavesDialogService(
            IWavesNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        /// <inheritdoc />
        public event EventHandler DialogsShown;

        /// <inheritdoc />
        public event EventHandler DialogsHidden;

        /// <inheritdoc />
        public override Task InitializeAsync()
        {
            if (IsInitialized)
            {
                return Task.CompletedTask;
            }

            IsInitialized = true;

            SubscribeEvents();

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task<WavesMessageDialogResult> ShowDialogAsync(
            IWavesMessageObject message,
            WavesMessageDialogButtons buttons = WavesMessageDialogButtons.Ok)
        {
            var result = await _navigationService
                .NavigateAsync<WavesMessageDialogViewModel, WavesMessageDialogParameter, WavesMessageDialogResult>(
                    new WavesMessageDialogParameter(message, buttons));

            return result;
        }

        /// <inheritdoc />
        public Task<WavesMessageDialogResult> ShowDialogAsync(
            string text,
            string title = "Message",
            IWavesObject sender = null,
            WavesMessageType type = WavesMessageType.Information,
            WavesMessageDialogButtons buttons = WavesMessageDialogButtons.Ok)
        {
            var message = new WavesTextMessage(text, title, sender, type);
            return ShowDialogAsync(message, buttons);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "WPF Dialog Service";
        }

        /// <inheritdoc />
        protected override void Dispose(
            bool disposing)
        {
            if (disposing)
            {
                UnsubscribeEvents();
            }

            // TODO: your code for release unmanaged resources.
        }

        /// <summary>
        ///     Notifies when dialogs shown.
        /// </summary>
        protected virtual void OnDialogsShown()
        {
            DialogsShown?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Notifies when all dialogs hidden.
        /// </summary>
        protected virtual void OnDialogsHidden()
        {
            DialogsHidden?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Subscribes to events.
        /// </summary>
        private void SubscribeEvents()
        {
            _navigationService.DialogsShown += OnNavigationServiceDialogsShown;
            _navigationService.DialogsHidden += OnNavigationServiceDialogsHidden;
        }

        /// <summary>
        ///     Unsubscribe from events.
        /// </summary>
        private void UnsubscribeEvents()
        {
            _navigationService.DialogsShown -= OnNavigationServiceDialogsShown;
            _navigationService.DialogsHidden -= OnNavigationServiceDialogsHidden;
        }

        /// <summary>
        ///     Notifies from navigation service that dialogs shown.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void OnNavigationServiceDialogsShown(
            object sender,
            EventArgs e)
        {
            OnDialogsShown();
        }

        /// <summary>
        ///     Notifies from navigation service that dialogs hidden.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void OnNavigationServiceDialogsHidden(
            object sender,
            EventArgs e)
        {
            OnDialogsHidden();
        }
    }
}