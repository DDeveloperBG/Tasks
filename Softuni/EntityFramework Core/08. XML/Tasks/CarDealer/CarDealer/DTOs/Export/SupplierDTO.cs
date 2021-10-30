using System.Xml.Serialization;

namespace CarDealer.DTOs.Export
{
    [XmlType("suplier")]
    public class SupplierDTO
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("parts-count")]
        public string PartsCount { get; set; }
    }
}
