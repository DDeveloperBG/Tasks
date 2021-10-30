using System.Xml.Serialization;

namespace ProductShop.DTOs
{
    [XmlType("Users")]
    public class Task8UsersDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        public Task8UserDTO[] users { get; set; }
    }
}
