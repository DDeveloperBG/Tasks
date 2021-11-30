using MUSACA.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MUSACA.Data.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public UserRole Role { get; set; }
    }
}
