using System.ComponentModel.DataAnnotations;
namespace SingkoFItnessWebApi.Dtos {
    public class ProgressLogsCreateDto {
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateOnly Date { get; set; }

        [Range(2, 500, ErrorMessage = "Weight must be between 2 kg and 500 kg.")]
        public decimal? Weight { get; set; }

        [Range(1, 100, ErrorMessage = "Body fat percentage must be between 1% and 100%.")]
        public decimal? BodyFatPercentage { get; set; }

        [Range(1, 500, ErrorMessage = "Muscle mass must be between 1 kg and 500 kg.")]
        public decimal? MuscleMass { get; set; }
    }
}