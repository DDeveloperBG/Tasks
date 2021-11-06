using System;
using System.Xml.Serialization;
using VaporStore.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.DTOs.Import
{
    [XmlType("Purchase")]
    public class PurchaseImportModel
    {
        [Required]
        [XmlAttribute("title")]
        public string GameName { get; set; }

        [Required]
        [EnumDataType(typeof(PurchaseType))]
        public string Type { get; set; }

        [Required]
        [XmlElement("Key")]
        [RegularExpression("[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}")]
        public string ProductKey { get; set; }

        [Required]
        [XmlElement("Card")]
        [RegularExpression(@"[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}")]
        public string CardNumber { get; set; }

        [Required]
        public string Date { get; set; }
    }
}
