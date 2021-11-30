using System.ComponentModel.DataAnnotations;

namespace MUSACA.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public long Barcode { get; set; }

        [Required]
        public string Picture { get; set; }
    }
}
