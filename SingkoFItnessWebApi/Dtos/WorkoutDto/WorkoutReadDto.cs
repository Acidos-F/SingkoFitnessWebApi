using SingkoFitnessWebApi.Dtos.Exercise;

namespace SingkoFItnessWebApi.Dtos {
    public class WorkoutReadDto {
        public int WorkoutId { get; set; }
        public string WorkoutName { get; set; } = null!;
        public DateOnly Date { get; set; }
        public int? DurationMinutes { get; set; }
        public decimal? CaloriesBurned { get; set; }

        public List<ExerciseReadDto> Exercises { get; set; } = new();
    }
}