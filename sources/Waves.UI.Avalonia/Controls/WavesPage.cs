using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Waves.Core.Base.Interfaces;
using Waves.UI.Avalonia.Extensions;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Interfaces;

namespace Waves.UI.Avalonia.Controls
{
    /// <summary>
    /// Page abstraction.
    /// </summary>
    public abstract class WavesPage : WavesUserControl
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesPage"/>.
        /// </summary>
        protected WavesPage()
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesPage"/>.
        /// </summary>
        /// <param name="core">Core.</param>
        /// <param name="navigationService">Instance of navigation service.</param>
        protected WavesPage(IWavesCore core, IWavesNavigationService navigationService)
            : base(core, navigationService)
        {
        }
    }
}
