using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Mobile.Data;
using Mobile.Data.Models;
using Mobile.Pages;
using Mobile.Services;
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
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");


                    fonts.AddFont("TangoSans.ttf", "TangoSans");
                    fonts.AddFont("TangoSans_Bold.ttf", "TangoSans_Bold");
                    fonts.AddFont("TangoSans_BoldItalic.ttf", "TangoSans_BoldItalic");
                    fonts.AddFont("TangoSans_Italic.ttf", "TangoSans_Italic");

                    fonts.AddFont("AgenorNeue-Regular.otf", "AgenorNeue");

                    fonts.AddFont("MaterialIconsRound-Regular.otf", "MaterialIcons");
                    fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialIconsOutlined");
                });

            builder.Services.AddDbContext<LocalDbContext>();
            builder.Services.AddSingleton<DbService>();

            builder.Services.AddSingleton<TasksPage>();
            builder.Services.AddSingleton<TasksViewModel>();

            builder.Services.AddSingleton<HistoryPage>();
            builder.Services.AddSingleton<HistoryViewModel>();

            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<SettingsViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
