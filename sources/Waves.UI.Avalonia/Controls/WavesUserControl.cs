﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Waves.Core.Base.Interfaces;
using Waves.UI.Avalonia.Extensions;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Interfaces;

namespace Waves.UI.Avalonia.Controls
{
    /// <summary>
    /// User control abstraction.
    /// </summary>
    public abstract class WavesUserControl : UserControl, IWavesView
    {
        private Dictionary<string, WavesContentControl> _regionContentControls;

        /// <summary>
        /// Creates new instance of <see cref="WavesUserControl"/>.
        /// </summary>
        protected WavesUserControl()
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesUserControl"/>.
        /// </summary>
        /// <param name="core">Core.</param>
        /// <param name="navigationService">Instance of navigation service.</param>
        protected WavesUserControl(IWavesCore core, IWavesNavigationService navigationService)
        {
            NavigationService = navigationService;
            Core = core;
        }

        /// <inheritdoc />
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Gets navigation service.
        /// </summary>
        protected IWavesNavigationService NavigationService { get; }

        /// <summary>
        /// Gets core.
        /// </summary>
        protected IWavesCore Core { get; }

        /// <inheritdoc />
        public virtual void RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            OnPropertyChanging(args);
        }

        /// <inheritdoc />
        public void RaisePropertyChanged(
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
            return Task.CompletedTask;
        }
        
        /// <summary>
        ///     Initializes components.
        /// </summary>
        protected void InitializeBaseControls()
        {
            _regionContentControls = this.FindRegions(NavigationService);
            this.InitializeControl(Core);
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
        /// <param name="disposing">Set
        ///     <value>true</value>
        ///     if you need to release managed and unmanaged resources. Set
        ///     <value>false</value>
        ///     if need to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

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
        /// Callback when property changing.
        /// </summary>
        /// <param name="e">Arguments.</param>
        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            PropertyChanging?.Invoke(this, e);
        }
    }
}
