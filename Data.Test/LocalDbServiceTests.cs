using Data.Models;
using SQLite;
using static SQLite.SQLite3;

namespace Data.Tests
{
    /// <summary>
    /// Unit tests for the LocalDbService class.
    /// </summary>
    public class LocalDbServiceTests
    {
        private string GetRandomFileName()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Test verifying that the SetItemAsync method correctly inserts an item into the database.
        /// </summary>
        [Fact]
        public async Task SetItemAsync_InsertsItemIntoDatabase()
        {
            // Arrange
            var databasePath = GetRandomFileName();
            var localDbService = new LocalDbService(new SQLiteAsyncConnection(databasePath));
            await localDbService.Init();

            var item = new Item { Id = 1, Name = "Test Item" };

            // Act
            var result = await localDbService.SetItemAsync(item);

            // Assert
            Assert.Equal(item, result);

            // Clean
            await localDbService.Close();
            File.Delete(databasePath);
        }

        /// <summary>
        /// Test verifying that the GetItemAsync method correctly retrieves an item from the database.
        /// </summary>
        [Fact]
        public async Task GetItemAsync_RetrievesItemFromDatabase()
        {
            // Arrange
            var databasePath = GetRandomFileName();
            var localDbService = new LocalDbService(new SQLiteAsyncConnection(databasePath));
            await localDbService.Init();

            var expectedItem = new Item { Id = 1, Name = "Test Item" };
            await localDbService.SetItemAsync(expectedItem);

            // Act
            var result = await localDbService.GetItemAsync(expectedItem.Id);

            // Assert
            Assert.Equal(expectedItem, result);

            // Clean
            await localDbService.Close();
            File.Delete(databasePath);
        }

        /// <summary>
        /// Test verifying that the SetItemAsync method correctly inserts an item with history into the database.
        /// </summary>
        [Fact]
        public async Task SetItemAsync_InsertsItemWithHistoryIntoDatabase()
        {
            // Arrange
            var databasePath = GetRandomFileName();
            var localDbService = new LocalDbService(new SQLiteAsyncConnection(databasePath));
            await localDbService.Init();
            var item = new Item { Id = 1, Name = "Test Item" };

            var history = new ItemHistory { Id = 1, ItemId = item.Id, Done = DateTime.Now.AddDays(-1) };
            item.History.Add(history);

            // Act
            var result = await localDbService.SetItemAsync(item);

            // Assert
            Assert.Equal(item, result);

            // Clean
            await localDbService.Close();
            File.Delete(databasePath);
        }

        /// <summary>
        /// Test verifying that the GetAllItemsAsync method returns items ordered by days since last occurrence.
        /// </summary>
        [Fact]
        public async Task GetAllItemsAsync_ReturnsItemsOrderedByDaysSinceLastOccurrence()
        {
            // Arrange
            var databasePath = GetRandomFileName();
            var localDbService = new LocalDbService(new SQLiteAsyncConnection(databasePath));
            await localDbService.Init();

            var item1 = new Item { Id = 1, Name = "Item 1", LoopInDays = 7 };
            var history1 = new ItemHistory { Id = 1, ItemId = item1.Id, Done = DateTime.Now.AddDays(-5) };
            item1.History.Add(history1);

            var item2 = new Item { Id = 2, Name = "Item 2", LoopInDays = 7 };
            var history2 = new ItemHistory { Id = 2, ItemId = item2.Id, Done = DateTime.Now.AddDays(-3) };
            item2.History.Add(history2);

            var item3 = new Item { Id = 3, Name = "Item 3", LoopInDays = 7 };
            var history3 = new ItemHistory { Id = 3, ItemId = item3.Id, Done = DateTime.Now.AddDays(-7) };
            item3.History.Add(history3);

            await localDbService.SetItemAsync(item1);
            await localDbService.SetItemAsync(item2);
            await localDbService.SetItemAsync(item3);

            // Act
            var result = await localDbService.GetAllItemsAsync();

            // Assert
            Assert.Equal(3, result.Count); 
            Assert.Equal(item3, result[0]);
            Assert.Equal(item1, result[1]);
            Assert.Equal(item2, result[2]);

            // Clean
            await localDbService.Close();
            File.Delete(databasePath);
        }

        [Fact]
        public async Task DeleteItemAsync_DeletesItemFromDatabaseAndThrowsKeyNotFoundExceptionWhenItemNotFound()
        {
            // Arrange
            var databasePath = GetRandomFileName();
            var localDbService = new LocalDbService(new SQLiteAsyncConnection(databasePath));
            await localDbService.Init();

            var item1 = new Item { Id = 1, Name = "Item 1", LoopInDays = 7 };
            var history1 = new ItemHistory { Id = 1, ItemId = item1.Id, Done = DateTime.Now.AddDays(-5) };
            item1.History.Add(history1);

            await localDbService.SetItemAsync(item1);
            var deleted = await localDbService.DeleteItemAsync(item1);

            // Assert
            Assert.True(deleted);
            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await localDbService.GetItemAsync(item1.Id));

            // Clean
            await localDbService.Close();
            File.Delete(databasePath);
        }
    }
}
