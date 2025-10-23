namespace StockOne.API.Model.DTOs
{
    public class CommentDTO
    {
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Author { get; set; } = string.Empty;
        public int StockId { get; set; }
    }
}
