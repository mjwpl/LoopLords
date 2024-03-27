using Microsoft.Extensions.Logging;
using Mobile.Data;
using Mobile.View;
using Mobile.ViewModel;

namespace Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddDbContext<LocalDbContext>();
            builder.Services.AddTransient<ItemListView>();
            builder.Services.AddTransient<ItemListViewModel>();
            builder.Services.AddTransient<IntroView>();

            var db = new LocalDbContext();
            db.Database.EnsureCreated();
            db.Dispose();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
