using System.ComponentModel.DataAnnotations;

namespace StockOne.API.Model
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Content should be minimum of 5 char.")]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public string Author { get; set; } = string.Empty;
        [Required]
        public int StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}
