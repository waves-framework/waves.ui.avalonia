using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;

namespace Waves.UI.Showcase.Avalonia.Android
{
    /// <summary>
    /// Main activity.
    /// </summary>
    [Activity(
        Label = "Waves.UI.Showcase.Avalonia.Android",
        Theme = "@style/MyTheme.NoActionBar",
        Icon = "@drawable/icon",
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : AvaloniaMainActivity
    {
    }
}
