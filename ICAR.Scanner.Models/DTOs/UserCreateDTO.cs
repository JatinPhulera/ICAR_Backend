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
    }
