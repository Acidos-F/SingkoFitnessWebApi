namespace SingkoFItnessWebApi.Models;

public partial class NutritionLog
{
    public int NutritionId { get; set; }

    public int UserId { get; set; }

    public DateOnly Date { get; set; }

    public string MealType { get; set; } = null!;

    public int Calories { get; set; }

    public decimal? Protein { get; set; }

    public decimal? Carbs { get; set; }

    public decimal? Fat { get; set; }

    public virtual User User { get; set; } = null!;
}