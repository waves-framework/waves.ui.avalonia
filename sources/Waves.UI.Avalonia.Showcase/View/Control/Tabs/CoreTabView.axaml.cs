using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Waves.Core.Base.Interfaces;
using Waves.Presentation.Interfaces;

namespace Waves.UI.Avalonia.Showcase.View.Control.Tabs
{
    /// <summary>
    /// Core tab view.
    /// </summary>
    public class CoreTabView : UserControl, IPresenterView
    {
        /// <summary>
        /// Creates new instance of <see cref="CoreTabView"/>.
        /// </summary>
        public CoreTabView()
        {
            this.InitializeComponent();
        }

        /// <inheritdoc />
        public event EventHandler<IMessage> MessageReceived;

        /// <summary>
        /// Initializes components.
        /// </summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        /// <summary>
        /// Notifies when message received.
        /// </summary>
        /// <param name="e">Message.</param>
        protected virtual void OnMessageReceived(IMessage e)
        {
            MessageReceived?.Invoke(this, e);
        }
    }
}
