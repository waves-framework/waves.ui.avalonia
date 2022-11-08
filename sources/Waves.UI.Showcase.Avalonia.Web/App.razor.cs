using Avalonia.Web.Blazor;

namespace Waves.UI.Showcase.Avalonia.Web;

/// <summary>
/// App.
/// </summary>
public partial class App
{
    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        WebAppBuilder.Configure<Avalonia.App>()
            .SetupWithSingleViewLifetime();
    }
}
