namespace Mobile.Helpers
{
    public static class ServiceHelper
    {
        public static TService GetService<TService>()
            => Current.GetService<TService>() ?? throw new SystemException("Current Platform application is null");

        public static IServiceProvider Current =>
#if ANDROID
            IPlatformApplication.Current?.Services ?? throw new SystemException("Current Platform application is null");
#elif IOS || MACCATALYST
            IPlatformApplication.Current?.Services ?? throw new SystemException("Current Platform application is null");
#elif WINDOWS10_0_17763_0_OR_GREATER
            MauiWinUIApplication.Current.Services;
#else
            throw new SystemException();
#endif
    }
}
