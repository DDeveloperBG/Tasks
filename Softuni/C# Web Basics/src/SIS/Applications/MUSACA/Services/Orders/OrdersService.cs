using MUSACA.Data;
using MUSACA.Data.Enums;
using MUSACA.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MUSACA.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext db;

        public OrdersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(int productId, int quantity, Guid cashierId)
        {
            Order order = new Order
            {
                ProductId = productId,
                Quantity = quantity,
                CashierId = cashierId
            };

            db.Orders.Add(order);

            db.SaveChanges();
        }

        public List<Order> GetNotCompletedUserOrders(Guid cashierId)
        {
            return this.db.Users
                .Where(x => x.Id == cashierId)
                .Select(x => x.Orders
                    .Where(y => y.Status == OrderStatus.Active)
                    .ToList())
                .FirstOrDefault();
        }

        public void CreateReceipt(Guid cashierId)
        {
            var receipt = new Receipt
            {
                CashierId = cashierId,
            };

            db.Receipts.Add(receipt);

            var activeOrders = GetNotCompletedUserOrders(cashierId);

            foreach (var order in activeOrders)
            {
                order.Status = OrderStatus.Completed;
                order.Receipt = receipt;
            }

            db.SaveChanges();
        }
    }
}
