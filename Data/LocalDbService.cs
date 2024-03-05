using Data.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace Data
{
    public class LocalDbService
    {
        private SQLiteAsyncConnection db;

        public LocalDbService(SQLiteAsyncConnection _connection)
        {
            //db = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DbName));
            db = _connection ?? throw new ArgumentNullException(nameof(_connection));
            db.ExecuteAsync("PRAGMA foreign_keys = ON;");
        }

        public async void Init()
        {
            await db.CreateTableAsync<Item>();
            await db.CreateTableAsync<ItemHistory>();
        }
    }
}
