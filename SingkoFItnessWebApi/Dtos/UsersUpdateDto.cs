using System.ComponentModel.DataAnnotations;

namespace SingkoFItnessWebApi.Dtos
{
    public class UsersUpdateDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public string Email { get; set; } = null!;

        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120 years.")]
        public int? Age { get; set; }

        [MaxLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
        public string? Gender { get; set; }

        [Range(30, 300, ErrorMessage = "Height must be between 30 cm and 300 cm.")]
        public decimal? Height { get; set; } // in cm

        [Range(2, 500, ErrorMessage = "Weight must be between 2 kg and 500 kg.")]
        public decimal? Weight { get; set; } // in kg
    }
}
