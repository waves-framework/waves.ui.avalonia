using Waves.Core.Base.Interfaces;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Presentation.Interfaces;

namespace Waves.UI.Avalonia.Plugins.Services.Interfaces
{
    /// <summary>
    /// Service for updating window styles.
    /// </summary>
    public interface IWavesWindowStyleService : IWavesService
    {
        /// <summary>
        /// Updates window style.
        /// </summary>
        /// <param name="window">Window.</param>
        void UpdateWindowStyle(IWavesWindow window);
    }
}
