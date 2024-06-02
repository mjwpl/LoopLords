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
                new Settings()
                {
                    Key = SettingsKeyEnum.RUN_COUNTER,
                    Value = "0"
                },
                new Settings()
                {
                    Key = SettingsKeyEnum.RUN_START_DATE,
                    Value = String.Empty
                },
                new Settings()
                {
                    Key = SettingsKeyEnum.RUN_LAST_DATE,
                    Value = String.Empty
                },
                new Settings()
                {
                    Key = SettingsKeyEnum.APP_GUID,
                    Value = String.Empty
                },
                new Settings()
                {
                    Key = SettingsKeyEnum.SHOW_HIDDEN,
                    Value = SettingsBoolEnum.FALSE.ToString()
                },
                new Settings()
                {
                    Key = SettingsKeyEnum.PUSH_NOTIFICATION,
                    Value = "22"
                },
                new Settings()
                {
                    Key = SettingsKeyEnum.DARK_MODE,
                    Value = SettingsBoolEnum.TRUE.ToString()
                }
            ];
        }
    }
}
