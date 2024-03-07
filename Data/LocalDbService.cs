using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace Data
{
    /// <summary>
    /// Provides functionality to interact with a local SQLite database.
    /// </summary>
    public class LocalDbService
    {
        private SQLiteAsyncConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalDbService"/> class.
        /// </summary>
        /// <param name="_connection">The SQLite connection to be used by the service.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="_connection"/> is null.</exception>
        public LocalDbService(SQLiteAsyncConnection _connection)
        {
            _db = _connection ?? throw new ArgumentNullException(nameof(_connection));
            _db.ExecuteAsync("PRAGMA foreign_keys = ON;");
        }

        /// <summary>
        /// Initializes the database tables asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Init()
        {
            await _db.CreateTableAsync<ItemHistory>();
            await _db.CreateTableAsync<Item>();
        }

        /// <summary>
        /// Inserts or updates an item asynchronously in the database along with its dependent objects.
        /// </summary>
        /// <param name="item">The item to be inserted or updated.</param>
        /// <returns>The inserted or updated item.</returns>
        public async Task<Item> SetItemAsync(Item item)
        {
            await _db.InsertWithChildrenAsync(item, recursive: true);
            return item;
        }

        /// <summary>
        /// Retrieves an item asynchronously from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the retrieved item.</returns>
        /// <exception cref="NotFoundException">Thrown when the provided ID does not correspond to any existing item in the database.</exception>
        public async Task<Item> GetItemAsync(int id)
            => await _db.GetAsync<Item>(id);

        /// <summary>
        /// Retrieves all items from the database asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of all items.</returns>
        public async Task<List<Item>> GetAllItems()
        {
            var items = await _db.GetAllWithChildrenAsync<Item>(recursive: true);
            return items.OrderByDescending(x => x.DaysSinceLastOccurrence).ToList();
        }

        /// <summary>
        /// Closes the connection to the database asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Close()
            => await _db.CloseAsync();
    }
}
