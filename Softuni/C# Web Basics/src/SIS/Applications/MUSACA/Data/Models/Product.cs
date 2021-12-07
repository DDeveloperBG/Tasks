using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MUSACA.Data.Models
{
    public class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string Barcode { get; set; }

        [Required]
        [Column("Picture")]
        public string PictureUrl { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
