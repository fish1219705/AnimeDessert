using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeDessert.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public required string ReviewNumber { get; set; }

        public required string ReviewContent { get; set; }

        public string? ReviewUser { get; set; }

        public required DateTime ReviewTime { get; set; }

        // A Review belongs to one Dessert
        [ForeignKey("Desserts")]
        public int DessertId { get; set; }
        public virtual Dessert? Dessert { get; set; }
    }
}
