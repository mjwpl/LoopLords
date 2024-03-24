using Mobile.Data.Models;

namespace Mobile.Data.Seed
{
    public static class DemoSeed
    {
        public static List<Item> GenerateItems()
        {
            return
            [
                new()
                {
                    Id = 1,
                    Name = "Wymiana oleju",
                    LoopInDays = 365,
                    DaysBeforeWarning = 14,
                    Description = "Coroczna wymiana oleju silnikowego w Toyocie"
                },
                new()
                {
                    Id = 2,
                    Name = "Trening na siłowni",
                    LoopInDays = 2,
                    DaysBeforeWarning = 1,
                    Description = "Trening obwodowy"
                },
                new()
                {
                    Id = 3,
                    Name = "Kwiaty dla dziewczyny",
                    LoopInDays = 30,
                    DaysBeforeWarning = 3,
                    Description = "Wiadomo...",
                }
            ];

        }

        public static List<ItemHistory> GenerateItemsHistory()
        {
            return
            [
                new()
                {
                    Id = 1,
                    ItemId = 1,
                    Done = DateTime.Now.AddDays(-340),
                },

                new()
                {
                    Id = 2,
                    ItemId = 2,
                    Done = DateTime.Now.AddDays(-1),
                },
                new()
                {
                    Id = 3,
                    ItemId = 2,
                    Done = DateTime.Now.AddDays(-4),
                },
                new()
                {
                    Id = 4,
                    ItemId = 2,
                    Done = DateTime.Now.AddDays(-7),
                },
                new()
                {
                    Id = 5,
                    ItemId = 2,
                    Done = DateTime.Now.AddDays(-11),
                },
                new()
                {
                    Id = 6,
                    ItemId = 2,
                    Done = DateTime.Now.AddDays(-16),
                },
                new()
                {
                    Id = 7,
                    ItemId = 2,
                    Done = DateTime.Now.AddDays(-20),
                },
                new()
                {
                    Id = 8,
                    ItemId = 2,
                    Done = DateTime.Now.AddDays(-30),
                },
                new()
                {
                    Id = 9,
                    ItemId = 2,
                    Done = DateTime.Now.AddDays(-50),
                },

                new()
                {
                    Id = 10,
                    ItemId = 3,
                    Done = DateTime.Now.AddDays(-40),
                },
                new()
                {
                    Id = 11,
                    ItemId = 3,
                    Done = DateTime.Now.AddDays(-90),
                }
            ];
        }
    }
}
