namespace SingkoFItnessWebApi.Models;

public partial class Workout
{
    public int WorkoutId { get; set; }

    public int UserId { get; set; }

    public string WorkoutName { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int? DurationMinutes { get; set; }

    public decimal? CaloriesBurned { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual User User { get; set; } = null!;
}