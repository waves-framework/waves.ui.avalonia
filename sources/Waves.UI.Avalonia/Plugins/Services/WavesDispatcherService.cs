using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using Waves.Core.Base;
using Waves.Core.Base.Attributes;
using Waves.UI.Plugins.Services.Interfaces;

namespace Waves.UI.Avalonia.Plugins.Services
{
    /// <summary>
    /// WPF Dispatcher service.
    /// </summary>
    [WavesService(typeof(IWavesDispatcherService))]
    public class WavesDispatcherService : WavesService, IWavesDispatcherService
    {
        /// <inheritdoc />
        public void Invoke(Action action)
        {
            Dispatcher.UIThread.Post(action);
        }

        /// <inheritdoc />
        public Task InvokeAsync(Action action)
        {
            return Dispatcher.UIThread.InvokeAsync(action);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "AvaloniaUI Dispatcher Service";
        }
    }
}
