using Mobile.Data.Enums;
using Mobile.Data.Models;

namespace Mobile.Data.Seed
{
    public static class SettingsSeed
    {
        public static List<Settings> Generate()
        {
            return
            [
                new Models.Settings()
                {
                    Key = SettingsKeyEnum.RUN_COUNTER,
                    Value = "0"
                },
                new Models.Settings()
                {
                    Key = SettingsKeyEnum.RUN_START_DATE,
                    Value = String.Empty
                },
                new Models.Settings()
                {
                    Key = SettingsKeyEnum.RUN_LAST_DATE,
                    Value = String.Empty
                },
                new Models.Settings()
                {
                    Key = SettingsKeyEnum.APP_GUID,
                    Value = String.Empty
                },
                new Models.Settings()
                {
                    Key = SettingsKeyEnum.SHOW_HIDDEN,
                    Value = "false"
                },
                new Models.Settings()
                {
                    Key = SettingsKeyEnum.PUSH_NOTIFICATION,
                    Value = "22"
                },
            ];
        }
    }
}
