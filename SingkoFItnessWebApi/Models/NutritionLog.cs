using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingkoFItnessWebApi.Models;

public partial class NutritionLog
{
    [Key]
    public int NutritionId { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public DateOnly Date { get; set; }
    [Required]
    [MaxLength(50)]
    public string MealType { get; set; } = null!;
    [Required]
    public int Calories { get; set; }

    public decimal? Protein { get; set; }

    public decimal? Carbs { get; set; }

    public decimal? Fat { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}