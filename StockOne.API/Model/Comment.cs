namespace StockOne.API.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Author { get; set; } = string.Empty;
        public int StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}
