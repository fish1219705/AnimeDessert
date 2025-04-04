using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeDessert.Models
{
    public class Instruction
    {
        [Key]
        public int InstructionId { get; set; }

        // An Instruction belongs to one Dessert
        [ForeignKey("Desserts")]
        public int DessertId { get; set; }
        public virtual Dessert? Dessert { get; set; }

        // An Instruction belongs to one Ingredient
        [ForeignKey("Ingredients")]
        public int IngredientId { get; set; }
        public virtual Ingredient? Ingredient { get; set; }

        public string? ChangeIngredientOption { get; set; }

        public required string QtyOfIngredient { get; set; }
    }

    public class InstructionDto
    {
        public int InstructionId { get; set; }
        public string? ChangeIngredientOption { get; set; }
        public required string QtyOfIngredient { get; set; }
        public int DessertId { get; set; }
        public int IngredientId { get; set; }

        //flattened from Instruction -> Dessert
        public string? DessertName { get; set; }

        //flattened from Instruction -> Ingredient
        public string? IngredientName { get; set; }
    }
}
