using Plugin.LocalNotification;

namespace Mobile.Helpers
{
    public class LocalPushNotification
    {
        public static void Register(int hour)
        {
            var req = new NotificationRequest
            {
                NotificationId = 31337,
                Title = "Loop Lords",
                Description = "Czy pamiętasz o dzisiejszych zadaniach?",
                Subtitle = "Oznacz zadania",
                Schedule = new NotificationRequestSchedule { NotifyTime = DateTime.Today.AddHours(22) }
            };

            LocalNotificationCenter.Current.Show(req);
        }
    }
}
