using System.Xml.Serialization;

namespace ProductShop.DTOs
{
    [XmlType("Category")]
    public class Task3CategoryDTO
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
