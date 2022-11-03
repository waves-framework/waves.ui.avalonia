using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;

namespace Waves.UI.Showcase.Avalonia.Android
{
    /// <summary>
    /// Main activitity.
    /// </summary>
    [Activity(
        Label = "AvaloniaApplication2.Android",
        Theme = "@style/MyTheme.NoActionBar",
        Icon = "@drawable/icon",
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : AvaloniaActivity<App>
    {
        /// <inheritdoc/>
        protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
        {
            return base.CustomizeAppBuilder(builder);
        }
    }
}
