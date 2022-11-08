using Android.App;
using Android.Content;
using Application = Android.App.Application;

namespace Waves.UI.Showcase.Avalonia.Android
{
    /// <summary>
    /// Splash activity.
    /// </summary>
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        /// <inheritdoc />
        protected override void OnResume()
        {
            base.OnResume();

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
