namespace MUSACA.ViewModels.Receipts
{
    public class ReceiptModel
    {
        public string Id { get; set; }

        public decimal Total { get; set; }

        public string IssuedOnDate { get; set; }

        public string Cashier { get; set; }
    }
}
