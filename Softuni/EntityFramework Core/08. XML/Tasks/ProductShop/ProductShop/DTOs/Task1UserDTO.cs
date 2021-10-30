using System.Xml.Serialization;

namespace ProductShop.DTOs
{
    [XmlType("User")]
    public class Task1UserDTO
    {
        [XmlElement(ElementName = "firstName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "lastName")]
        public string LastName { get; set; }

        [XmlElement(ElementName = "age")]
        public int Age { get; set; }
    }
}
