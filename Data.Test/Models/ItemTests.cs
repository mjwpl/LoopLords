using Data.Models;
using SQLite;

namespace Data.Test.Models
{
    public class ItemTests
    {
        [Fact]
        public void Constructor_ThrowsException_WhenLoopInDaysIsEqualToOne()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => new Item { Id = 1, Name = "Test Item", LoopInDays = 1 });
        }

        [Fact]
        public void Item_SetDaysBeforeWarning_ThrowsException_WhenValueIsGreaterThanOrEqualToLoopInDays()
        {
            // Arrange
            var act = () => new Item { Id = 1, Name = "Test Item", LoopInDays = 7, DaysBeforeWarning = 7 };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void DaysSinceLastOccurrence_ReturnsCorrectValue()
        {
            // Arrange
            var item = new Item { Id = 1, Name = "Test Item" };
            var history = new ItemHistory { Id = 1, ItemId = item.Id, Done = DateTime.Now.AddDays(-3) };
            item.History = [history];

            // Act
            var result = item.DaysSinceLastOccurrence;

            // Assert
            Assert.Equal(3, result);
        }
    }
}
