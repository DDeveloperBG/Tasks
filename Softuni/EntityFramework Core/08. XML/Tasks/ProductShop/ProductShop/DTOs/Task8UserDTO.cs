using System.Xml.Serialization;

namespace ProductShop.DTOs
{
    [XmlType("User")]
    public class Task8UserDTO
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        public Task8SoldProductsDTO SoldProducts { get; set; }
    }
}
