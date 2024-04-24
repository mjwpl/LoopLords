using Microsoft.Extensions.Logging;
using Mobile.Data;
using Mobile.Data.AutoMapperProfiles;
using Mobile.Pages;
using Mobile.ViewModel;
using CommunityToolkit.Maui;

namespace Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FaSolid");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FaRegular");

                    fonts.AddFont("TangoSans.ttf", "TangoSans");
                    fonts.AddFont("TangoSans_Bold.ttf", "TangoSans_Bold");
                    fonts.AddFont("TangoSans_BoldItalic.ttf", "TangoSans_BoldItalic");
                    fonts.AddFont("TangoSans_Italic.ttf", "TangoSans_Italic");

                    fonts.AddFont("AgenorNeue-Regular.otf", "AgenorNeue");
                });

            builder.Services.AddAutoMapper(typeof(ItemProfile));
            builder.Services.AddDbContext<LocalDbContext>();
            builder.Services.AddTransient<TodayViewModel>();
            builder.Services.AddTransient<TasksViewModel>();
            builder.Services.AddTransient<IntroView>();
            builder.Services.AddTransient<NewViewModel>();


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
