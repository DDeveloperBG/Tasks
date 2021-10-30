using System.Xml.Serialization;

namespace ProductShop.DTOs
{
    [XmlType("Product")]
    public class Task5ProductDTO
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "price")]
        public decimal Price { get; set; }

        [XmlElement(ElementName = "buyer")]
        public string Buyer { get; set; }
    }
}
