using System.Xml.Serialization;

namespace ProductShop.DTOs
{
    [XmlType("Product")]
    public class Task2ProductDTO
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "price")]
        public decimal Price { get; set; }

        [XmlElement(ElementName = "sellerId")]
        public int SellerId { get; set; }

        [XmlElement(ElementName = "buyerId")]
        public int? BuyerId { get; set; }
    }
}
