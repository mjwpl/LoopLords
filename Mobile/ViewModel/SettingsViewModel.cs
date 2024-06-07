using CommunityToolkit.Mvvm.ComponentModel;
using Mobile.Services;
using Mobile.Data.Enums;

namespace Mobile.ViewModel
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly DbService DbService;

        [ObservableProperty]
        private bool _isShowHidden = false;

        [ObservableProperty]
        private bool _isDarkMode = false;

        [ObservableProperty]
        private bool _isPushNotification = false;


        public SettingsViewModel(DbService dbService)
        {
            DbService = dbService;

            var setting = DbService.GetSettings(SettingsKeyEnum.SHOW_HIDDEN);
            _isShowHidden = EnumerableExtensions.GetSettingsBoolValueEnum(setting);

            setting = DbService.GetSettings(SettingsKeyEnum.DARK_MODE);
            _isDarkMode         = EnumerableExtensions.GetSettingsBoolValueEnum(setting);

            _isPushNotification = DbService.GetSettings(SettingsKeyEnum.PUSH_NOTIFICATION) != ""; // TODO: Change this to a real value
        }

        partial void OnIsShowHiddenChanged(bool value)
        {
            DbService.SetSettings(SettingsKeyEnum.SHOW_HIDDEN, value ? "1" : "0");
        }

        partial void OnIsDarkModeChanged(bool value)
        {
            DbService.SetSettings(SettingsKeyEnum.DARK_MODE, value ? "1" : "0");
        }

        partial void OnIsPushNotificationChanged(bool value)
        {
            DbService.SetSettings(SettingsKeyEnum.PUSH_NOTIFICATION, value ? "22" : ""); // TODO: Change this to a real value
        }
    }
}
