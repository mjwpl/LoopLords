using Mobile.Data.Enums;
using Mobile.Data.Helpers;
using Mobile.Helpers;
using Mobile.Pages;
using Mobile.Services;


namespace Mobile
{
    public partial class App : Application
    {
        private readonly DbService _dbService;

        public App(DbService db, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            UserAppTheme = AppTheme.Dark;

            _dbService = db;
            IsFirstRun();

            var push = _dbService.GetSettings(SettingsKeyEnum.PUSH_NOTIFICATION);
            if (!String.IsNullOrEmpty(push))
                LocalPushNotification.Register(int.Parse(push));

            MainPage = new MainPage(
                serviceProvider.GetRequiredService<TasksPage>(),
                serviceProvider.GetRequiredService<SettingsPage>()
                );
        }

        private bool IsFirstRun()
        {
            var runCounter = _dbService.GetSettings(SettingsKeyEnum.RUN_COUNTER);

            if (runCounter == "0")
            {
                _dbService.SetSettings(SettingsKeyEnum.RUN_COUNTER, "1");
                _dbService.SetSettings(SettingsKeyEnum.RUN_START_DATE, DateTime.UtcNow.ToString());
                _dbService.SetSettings(SettingsKeyEnum.RUN_LAST_DATE, DateTime.UtcNow.ToString());
                _dbService.SetSettings(SettingsKeyEnum.APP_GUID, Guid.NewGuid().ToString());

                return true;
            }
            else
            {
                _dbService.SetSettings(SettingsKeyEnum.RUN_COUNTER, Value.Increment(runCounter));
                _dbService.SetSettings(SettingsKeyEnum.RUN_LAST_DATE, DateTime.UtcNow.ToString());
                return false;
            }
        }
    }
}
