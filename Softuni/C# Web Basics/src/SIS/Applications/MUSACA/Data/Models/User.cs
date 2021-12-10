using SIS.WebServer.DataManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MUSACA.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Id = Guid.NewGuid();
            this.Role = UserRole.User;
            this.Receipts = new HashSet<Receipt>();
            this.Orders = new HashSet<Order>();
        }

        [Required]
        [MaxLength(300)]
        public string Password { get; set; }

        public ICollection<Receipt> Receipts { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
