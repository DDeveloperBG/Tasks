using System.Xml.Serialization;

namespace ProductShop.DTOs
{
    [XmlType("CategoryProduct")]
    public class Task4CategoryProductDTO
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
    }
}
