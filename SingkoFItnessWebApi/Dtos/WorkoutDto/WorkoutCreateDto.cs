using System.ComponentModel.DataAnnotations;

namespace SingkoFItnessWebApi.Dtos
{
    public class WorkoutCreateDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be greater than 0.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Workout name is required.")]
        [MaxLength(200, ErrorMessage = "Workout name cannot exceed 200 characters.")]
        public string WorkoutName { get; set; } = null!;

        [Required(ErrorMessage = "Date is required.")]
        public DateOnly Date { get; set; }

        [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes.")]
        public int? DurationMinutes { get; set; }

        [Range(0, 20000, ErrorMessage = "Calories burned must be between 0 and 20000.")]
        public decimal? CaloriesBurned { get; set; }
    }
}