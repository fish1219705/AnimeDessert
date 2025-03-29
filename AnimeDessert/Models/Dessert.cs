using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeDessert.Models
{
    public class Dessert
    {
        [Key]
        public int DessertId { get; set; }

        public required string DessertName { get; set; }

        public required string DessertDescription { get; set; }

        public string? SpecificTag { get; set; }

        // A Dessert can have many Ingredients
        public ICollection<Ingredient>? Ingredients { get; set; }

        // A Dessert can have many Instructions
        public ICollection<Instruction>? Instructions { get; set; }

        // A Dessert can have many Reviews
        public ICollection<Review>? Reviews { get; set; }

        // A Dessert belongs to zero or one Character
        [ForeignKey("Characters")]
        public int? CharacterId { get; set; }
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public virtual Character? Character { get; set; }

        // A Dessert can have many Images
        public ICollection<Image>? Images { get; set; }
    }

    public class DessertDto
    {
        public int DessertId { get; set; }
        public string DessertName { get; set; }
        public string DessertDescription { get; set; }
        public string SpecificTag { get; set; }

    }
}
