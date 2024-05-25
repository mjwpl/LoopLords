using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Mobile.Data;
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

            builder.Services.AddSingleton<TasksViewModel>(sp => new TasksViewModel()
            {
                _dbService = sp.GetRequiredService<DbService>()
            });

            builder.Services.AddSingleton<TasksPage>(sp => new TasksPage(sp.GetRequiredService<TasksViewModel>())
            {
            
            });

            builder.Services.AddSingleton<IntroView>();
            builder.Services.AddSingleton<AddViewModel>();
            

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
