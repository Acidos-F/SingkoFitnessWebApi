using System.ComponentModel.DataAnnotations;

namespace SingkoFItnessWebApi.Dtos
{
    public class UsersReadDto
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int? Age { get; set; }

        public string? Gender { get; set; }

        public decimal? Height { get; set; } // cm

        public decimal? Weight { get; set; } // kg

        public DateTime? CreatedAt { get; set; }

        public string RoleName { get; set; } = null!;
    }
}