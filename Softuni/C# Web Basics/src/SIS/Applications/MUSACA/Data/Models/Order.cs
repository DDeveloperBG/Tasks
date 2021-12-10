using MUSACA.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MUSACA.Data.Models
{
    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid();
            Status = OrderStatus.Active;
        }

        [Key]
        public Guid Id { get; set; }

        public OrderStatus Status { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public Guid? ReceiptId { get; set; }
        public Receipt Receipt { get; set; }

        public Guid CashierId { get; set; }
        public User Cashier { get; set; }
    }
}
