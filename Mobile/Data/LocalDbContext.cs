using Data.Models;
using Microsoft.EntityFrameworkCore;
using Mobile.Data.Seed;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>().HasData(Demo.GenerateItems());
            modelBuilder.Entity<ItemHistory>().HasData(Demo.GenerateItemsHistory());
        }

        public static string GetPath(string nameDb)
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
