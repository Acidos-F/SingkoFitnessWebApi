namespace SingkoFItnessWebApi.Models;

public partial class Exercise
{
    public int ExerciseId { get; set; }

    public int WorkoutId { get; set; }

    public string ExerciseName { get; set; } = null!;

    public int? Sets { get; set; }

    public int? Reps { get; set; }

    public decimal? WeightUsed { get; set; }

    public decimal? DistanceKm { get; set; }

    public int? DurationMinutes { get; set; }

    public virtual Workout Workout { get; set; } = null!;
}