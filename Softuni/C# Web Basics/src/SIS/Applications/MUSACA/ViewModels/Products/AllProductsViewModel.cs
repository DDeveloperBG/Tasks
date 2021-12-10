using System.Collections.Generic;
using System.Linq;

namespace MUSACA.ViewModels.Products
{
    public class AllProductsViewModel
    {
        public AllProductsViewModel()
        {
            Products = new List<ProductViewModel>();
        }

        public List<ProductViewModel> Products { get; set; }

        public decimal Price => Products.Sum(x => x.Price);
    }
}
