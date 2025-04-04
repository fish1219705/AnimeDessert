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

    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public required string ReviewNumber { get; set; }
        public required string ReviewContent { get; set; }
        public DateTime ReviewTime { get; set; }
        public string? ReviewUser { get; set; }
        public int DessertId { get; set; }

        // flattened from Review -> Dessert
        public string? DessertName { get; set; }
    }
}
