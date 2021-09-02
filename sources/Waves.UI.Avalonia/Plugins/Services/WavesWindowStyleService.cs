using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using Waves.Core.Base;
using Waves.Core.Base.Attributes;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Avalonia.Extensions;
using Waves.UI.Avalonia.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Interfaces;

namespace Waves.UI.Avalonia.Plugins.Services
{
    /// <summary>
    /// Window style service.
    /// </summary>
    [WavesService(typeof(IWavesWindowStyleService))]
    public class WavesWindowStyleService : WavesService, IWavesWindowStyleService
    {
        private StyleInclude _styleInclude;
        
        /// <inheritdoc />
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            _styleInclude = StyledElementExtensions.GetStyle(Constants.GenericDictionaryUri);
        }

        /// <inheritdoc />
        public void UpdateWindowStyle(IWavesWindow window)
        {
            ////window.Classes.Add("Default");
        }
    }
}
