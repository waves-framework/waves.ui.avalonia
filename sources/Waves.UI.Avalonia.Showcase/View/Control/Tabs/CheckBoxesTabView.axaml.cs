using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Waves.Core.Base.Interfaces;
using Waves.Presentation.Interfaces;

namespace Waves.UI.Avalonia.Showcase.View.Control.Tabs
{
    /// <summary>
    /// CheckBox tab view.
    /// </summary>
    public class CheckBoxesTabView : UserControl, IPresenterView
    {
        /// <summary>
        /// Creates new instance of <see cref="CheckBoxesTabView"/>.
        /// </summary>
        public CheckBoxesTabView()
        {
            InitializeComponent();
        }
        
        /// <inheritdoc />
        public event EventHandler<IWavesMessage> MessageReceived;

        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();
        
        /// <inheritdoc />
        public IWavesCore Core { get; protected set; }
        
        /// <inheritdoc />
        public void AttachCore(IWavesCore core)
        {
            Core = core;
        }
        
        /// <summary>
        /// Notifies when message received.
        /// </summary>
        /// <param name="e">Message.</param>
        protected virtual void OnMessageReceived(IWavesMessage e)
        {
            MessageReceived?.Invoke(this, e);
        }
        
        /// <summary>
        /// Initializes components.
        /// </summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
