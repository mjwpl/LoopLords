namespace Mobile.Helpers
{
    public static class ServiceHelper
    {
        public static TService GetService<TService>()
#pragma warning disable CS8603
            => Current.GetService<TService>();
#pragma warning restore CS8603

        public static IServiceProvider Current =>
#if ANDROID
            MauiApplication.Current.Services;
#elif IOS || MACCATALYST
            MauiUIApplicationDelegate.Current.Services;
#elif WINDOWS10_0_17763_0_OR_GREATER
            MauiWinUIApplication.Current.Services;
#else
            null;
#endif
    }

}
