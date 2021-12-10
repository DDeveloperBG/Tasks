using MUSACA.ViewModels.Receipts;
using System;
using System.Collections.Generic;

namespace MUSACA.Services.Receipts
{
    public interface IReceiptsService
    {
        public List<ReceiptModel> GetUserReceipts(Guid userId);
       
        public ReceiptDetailsViewModel GetUserReceipt(Guid userId, string receiptId);

        public ReceiptDetailsViewModel GetReceipt(string receiptId);

        public List<ReceiptModel> GetAllReceipts();
    }
}
