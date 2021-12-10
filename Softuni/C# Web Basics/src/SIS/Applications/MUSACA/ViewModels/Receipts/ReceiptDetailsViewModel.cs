using MUSACA.ViewModels.Home;
using System.Collections.Generic;
using System.Linq;

namespace MUSACA.ViewModels.Receipts
{
    public class ReceiptDetailsViewModel
    {
        public List<HomeProductViewModel> Orders { get; set; }

        public decimal Total => Orders.Sum(x => x.Price * x.Quantity);

        public string IssuedOn { get; set; }

        public string Cashier { get; set; }

        public string Receipt { get; set; }
    }
}
