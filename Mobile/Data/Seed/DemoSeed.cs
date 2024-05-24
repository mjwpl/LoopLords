using Mobile.Data.Models;
using Mobile.Helpers;

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
                    HideInDays = 350,
                    Icon = MaterialIcons.Oil_barrel,
                    Color = "#1d72a0"
                },
                new()
                {
                    Id = 2,
                    Name = "Trening na siłowni",
                    Icon = MaterialIcons.Fitness_center,
                },
                new()
                {
                    Id = 3,
                    Name = "Kwiaty dla dziewczyny",
                    Icon = MaterialIcons.Local_florist,
                    Color = "#9117f1"
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
                    Done = DateTime.Now.AddDays(-30),
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
