using MUSACA.Data;
using MUSACA.ViewModels.Home;
using MUSACA.ViewModels.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MUSACA.Services.Receipts
{
    public class ReceiptsService : IReceiptsService
    {
        private readonly ApplicationDbContext db;

        public ReceiptsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ReceiptDetailsViewModel GetUserReceipt(Guid userId, string receiptId)
        {
            Guid id = new Guid(receiptId);

            return db.Receipts
                  .Where(x => x.CashierId == userId && x.Id == id)
                  .Select(x => new ReceiptDetailsViewModel
                  {
                      Receipt = receiptId,
                      Cashier = x.Cashier.Username,
                      IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy"),
                      Orders = x.Orders
                        .Select(y => new HomeProductViewModel
                        {
                            Name = y.Product.Name,
                            Price = y.Product.Price,
                            Quantity = y.Quantity
                        })
                        .ToList()
                  })
                  .SingleOrDefault();
        }

        public ReceiptDetailsViewModel GetReceipt(string receiptId)
        {
            Guid id = new Guid(receiptId);

            return db.Receipts
                  .Where(x => x.Id == id)
                  .Select(x => new ReceiptDetailsViewModel
                  {
                      Receipt = receiptId,
                      Cashier = x.Cashier.Username,
                      IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy"),
                      Orders = x.Orders
                        .Select(y => new HomeProductViewModel
                        {
                            Name = y.Product.Name,
                            Price = y.Product.Price,
                            Quantity = y.Quantity
                        })
                        .ToList()
                  })
                  .SingleOrDefault();
        }

        public List<ReceiptModel> GetUserReceipts(Guid userId)
        {
            return db.Receipts
                .Where(x => x.CashierId == userId)
                .Select(x => new ReceiptModel
                {
                    Id = x.Id.ToString(),
                    Total = x.Orders.Sum(o => o.Product.Price),
                    IssuedOnDate = x.IssuedOn.ToString("dd/MM/yyyy"),
                    Cashier = x.Cashier.Username
                })
                .ToList();
        }

        public List<ReceiptModel> GetAllReceipts()
        {
            return db.Receipts
                .Select(x => new ReceiptModel
                {
                    Id = x.Id.ToString(),
                    Total = x.Orders.Sum(o => o.Product.Price),
                    IssuedOnDate = x.IssuedOn.ToString("dd/MM/yyyy"),
                    Cashier = x.Cashier.Username
                })
                .ToList();
        }
    }
}
