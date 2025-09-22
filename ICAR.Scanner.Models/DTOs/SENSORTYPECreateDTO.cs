using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs;
    public class SENSORTYPECreateDTO
{
        [Required]
        public string Username { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
         public string? PhoneNumber { get; set; } = null!;

        [Required]
        public int? RoleID { get; set; } = null!;

        public string? FirstName { get; set; } = null!;

        public string? LastName { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public bool? IsActive { get; set; } = null!;
        public DateTime? LastLoginAt { get; set; } = null!;
        public string AdminID { get; set; } = null!;
        public string State { get; set; } = null!;
        public string LastAccessTime { get; set; } = null!;
        public string Latitude { get; set; } = null!;
        public string Longitude { get; set; } = null!;
}
