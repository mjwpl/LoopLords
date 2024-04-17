namespace Mobile.Data.Models
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int LoopInDays { get; set; }
        public int? DaysSinceLastOccurrence { get; set; }
        public string NextScheduledDate { get; set; } = String.Empty;
    }
}
