using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockOne.API.Model
{
    public class Stock
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="Symbol should be minimum of 3 char.")]
        [MaxLength(8,ErrorMessage ="Symbol should be maximum of 8 char.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Company Name should be minimum of 3 char.")]
        [MaxLength(25, ErrorMessage = "Company Name should be maximum of 8 char.")]
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }
        [Required]
        public string Industry { get; set; } = string.Empty;
        [Required]
        public long MarketCap { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
