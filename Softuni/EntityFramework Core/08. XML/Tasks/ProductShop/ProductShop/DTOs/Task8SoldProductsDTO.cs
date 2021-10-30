using System.Xml.Serialization;

namespace ProductShop.DTOs
{
    [XmlType("SoldProducts")]
    public class Task8SoldProductsDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        public Task6ProductDTO[] products { get; set; }
    }
}
