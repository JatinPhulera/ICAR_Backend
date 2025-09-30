using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs;
    public class UserCreateDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
         public string? PhoneNumber { get; set; } = null!;

        public Guid? RoleID { get; set; }

        public Guid? InstitutionID { get; set; }

         public string? FirstName { get; set; } = null!;

        public string? LastName { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public bool? IsActive { get; set; } = null!;
        public DateTime? LastLoginAt { get; set; } = null!;
        public string AdminID { get; set; } = null!;
        public string State { get; set; } = null!;
        public DateTime LastAccessTime { get; set; } 
        public string Latitude { get; set; } = null!;
        public string Longitude { get; set; } = null!;
}
