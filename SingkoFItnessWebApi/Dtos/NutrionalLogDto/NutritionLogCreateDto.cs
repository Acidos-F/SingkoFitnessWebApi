using System.ComponentModel.DataAnnotations;

namespace SingkoFItnessWebApi.Dtos.NutrionalLogDto
{
    public class NutritionLogCreateDto
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Meal type cannot exceed 50 characters.")]
        public string MealType { get; set; } 

        [Required]
        [Range(0, 10000, ErrorMessage = "Calories must be between 0 and 10,000.")]
        public int Calories { get; set; }

        [Range(0, 1000, ErrorMessage = "Protein must be between 0 and 1000 grams.")]
        public decimal? Protein { get; set; }

        [Range(0, 1000, ErrorMessage = "Carbs must be between 0 and 1000 grams.")]
        public decimal? Carbs { get; set; }

        [Range(0, 1000, ErrorMessage = "Fat must be between 0 and 1000 grams.")]
        public decimal? Fat { get; set; }
    }
}
