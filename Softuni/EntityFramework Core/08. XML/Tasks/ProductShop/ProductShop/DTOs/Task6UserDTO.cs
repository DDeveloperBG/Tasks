using System.Xml.Serialization;

namespace ProductShop.DTOs
{
    [XmlType("User")]
    public class Task6UserDTO
    {
        [XmlElement(ElementName = "firstName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "lastName")]
        public string LastName { get; set; }

        [XmlElement(ElementName = "soldProducts")]
        public Task6ProductDTO[] SoldProducts { get; set; }
    }
}
