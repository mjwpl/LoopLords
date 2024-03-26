using Mobile.Data;
using Mobile.Data.Enums;
using Mobile.Data.Helpers;
using Mobile.Data.Models;
using Mobile.View;

namespace Mobile
{
    public partial class App : Application
    {
        LocalDbContext _db;
        public App(LocalDbContext db)
        {
            InitializeComponent();
            _db = db;

            MainPage = (FirstRun()) ? new IntroView() : new AppShell();
         
        }

        private bool FirstRun()
        {
            var ft = _db.Settings.FirstOrDefault(x => x.Key == SettingsKeyEnum.RUN_COUNTER);

            if (ft != null)
            {
                if (ft.Value == "0")
                {
                    ft.Value = "1";
                    _db.SaveChanges();

                    return true;
                }
                else
                {
                    ft.Value = Value.Increment(ft.Value);

                    var lastDate = _db.Settings.FirstOrDefault(x => x.Key == SettingsKeyEnum.RUN_LAST_DATE);
                    if (lastDate != null)
                    {
                        lastDate.Value = DateTime.Now.ToString();
                        _db.SaveChanges();

                        
                    }

                    return false;
                }
            }

            return true;
        }
    }
}
