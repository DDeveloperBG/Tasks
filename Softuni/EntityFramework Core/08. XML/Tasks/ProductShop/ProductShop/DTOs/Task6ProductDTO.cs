using System.Xml.Serialization;

namespace ProductShop.DTOs
{
    [XmlType("Product")]
    public class Task6ProductDTO
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
