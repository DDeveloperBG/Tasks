using System;
using System.ComponentModel.DataAnnotations;

namespace SIS.WebServer.DataManager
{
    public class IdentityUser
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Username { get; set; }

        [Required]
        [MaxLength(300)]
        public string Email { get; set; }

        public UserRole Role { get; set; }
    }
}
