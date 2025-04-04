using System.ComponentModel.DataAnnotations;

namespace AnimeDessert.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        public required string IngredientName { get; set; }

        public string? IngredientDescription { get; set; }

        // An Ingredient can have many Instructions
        public ICollection<Instruction>? Instructions { get; set; }

        // An Ingredient can appear in many Desserts
        public ICollection<Dessert>? Desserts { get; set; }
    }

    public class IngredientDto
    {
        public int IngredientId { get; set; }
        public required string IngredientName { get; set; }
        public string? IngredientDescription { get; set; }

    }
}
