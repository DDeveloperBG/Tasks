using MUSACA.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MUSACA.Data.Models
{
    public class User
    {
        public User()
        {
            Receipts = new HashSet<Receipt>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Username { get; set; }

        [Required]
        [MaxLength(300)]
        public string Password { get; set; }

        [Required]
        [MaxLength(300)]
        public string Email { get; set; }

        public UserRole Role { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}
