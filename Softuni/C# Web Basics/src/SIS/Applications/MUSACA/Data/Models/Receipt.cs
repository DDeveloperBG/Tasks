using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MUSACA.Data.Models
{
    public class Receipt
    {
        public Receipt()
        {
            Orders = new HashSet<Order>();
            IssuedOn = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public Guid CashierId { get; set; }
        public User Cashier { get; set; }
        
        public ICollection<Order> Orders { get; set; }
    }
}
