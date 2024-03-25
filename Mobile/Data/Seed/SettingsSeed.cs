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
                    Value = DateTime.Now.ToString()
                },
                new Models.Settings()
                {
                    Key = SettingsKeyEnum.RUN_LAST_DATE,
                    Value = DateTime.Now.ToString()
                }
            ];
        }
    }
}
