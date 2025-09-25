namespace SingkoFitnessWebApi.Dtos.Exercise
{
    public class ExerciseReadDto
    {
        public int ExerciseId { get; set; }
        public int WorkoutId { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public decimal? WeightUsed { get; set; }
        public decimal? DistanceKm { get; set; }
        public int? DurationMinutes { get; set; }
    }
}