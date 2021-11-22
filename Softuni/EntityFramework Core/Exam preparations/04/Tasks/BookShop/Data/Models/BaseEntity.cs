using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
