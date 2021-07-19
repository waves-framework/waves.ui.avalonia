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

namespace Waves.UI.Avalonia.Plugins.Services
{
    /// <summary>
    /// Window style service.
    /// </summary>
    [WavesService(typeof(IWindowStyleService))]
    public class WindowStyleService : WavesService, IWindowStyleService
    {
        private StyleInclude _styleInclude;
        
        /// <inheritdoc />
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            _styleInclude = StyledElementExtensions.GetStyle(Constants.GenericDictionaryUri);
        }

        /// <inheritdoc />
        public void UpdateWindowStyle(WavesWindow window)
        {
            // TODO: remove?
            // var done = _styleInclude.TryGetResource("WindowStyle", out var style);
            // if (done && style is Style windowStyle)
            // {
            //     window.Styles.Add(windowStyle);
            // }
        }
    }
}
