using System.Collections.Generic;
using System.Linq;

namespace MUSACA.ViewModels.Home
{
    public class HomeProductsViewModel
    {
        public HomeProductsViewModel()
        {
            Products = new List<HomeProductViewModel>();
        }

        public List<HomeProductViewModel> Products { get; set; }

        public decimal Price => Products.Sum(x => x.Price * x.Quantity);
    }
}
