using System;

namespace MUSACA.Services.Orders
{
    public interface IOrdersService
    {
        public void Create(int productId, int quantity, Guid cashierId);

        public void CreateReceipt(Guid cashierId);
    }
}
