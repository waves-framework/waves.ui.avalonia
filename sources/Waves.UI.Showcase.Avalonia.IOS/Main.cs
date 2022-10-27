namespace Waves.UI.Showcase.Avalonia.IOS
{
    /// <summary>
    /// Application.
    /// </summary>
#pragma warning disable SA1649
    public static class Application
#pragma warning restore SA1649
    {
        /// <summary>
        /// This is the main entry point of the application.
        /// </summary>
        /// <param name="args">Args.</param>
        public static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, typeof(AppDelegate));
        }
    }
}
