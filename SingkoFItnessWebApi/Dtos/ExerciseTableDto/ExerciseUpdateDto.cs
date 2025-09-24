using System.ComponentModel.DataAnnotations;
namespace SingkoFitnessWebApi.Dtos.Exercise
{
    public class ExerciseUpdateDto
    {
        [Required(ErrorMessage = "Workout ID is required.")]
        public int WorkoutId { get; set; }

        [Required(ErrorMessage = "Exercise name is required.")]
        [MaxLength(100, ErrorMessage = "Exercise name cannot exceed 100 characters.")]
        public string ExerciseName { get; set; } = null!;

        [Range(1, 100, ErrorMessage = "Sets must be between 1 and 100.")]
        public int? Sets { get; set; }

        [Range(1, 1000, ErrorMessage = "Reps must be between 1 and 1000.")]
        public int? Reps { get; set; }

        [Range(0, 1000, ErrorMessage = "Weight used must be between 0 and 1000 kg.")]
        public decimal? WeightUsed { get; set; }

        [Range(0, 1000, ErrorMessage = "Distance must be between 0 and 1000 km.")]
        public decimal? DistanceKm { get; set; }

        [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes.")]
        public int? DurationMinutes { get; set; }
    }
}