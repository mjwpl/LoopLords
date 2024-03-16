using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Mobile.Data
{
    public class LocalDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemHistory> History { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string conn = $"Filename={GetPath("moj.db")}";
            optionsBuilder.UseSqlite(conn);
        }

        private static string GetPath(string nameDb)
        {
            string pathDbSqlite = string.Empty;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                pathDbSqlite = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                pathDbSqlite = Path.Combine(pathDbSqlite, nameDb);

            }

            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                pathDbSqlite = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                pathDbSqlite = Path.Combine(pathDbSqlite, "..", "Library", nameDb);
            }

            return pathDbSqlite;
        }
    }
}
