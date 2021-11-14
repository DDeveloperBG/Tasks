using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

using SoftJail.Data.Models.Enums;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class OfficerImportModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [XmlElement("Money")]
        [Range(0, int.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        [EnumDataType(typeof(Position))]
        public string Position { get; set; }

        [Required]
        [EnumDataType(typeof(Weapon))]
        public string Weapon { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public PrisonerId[] Prisoners { get; set; }
    }

    [XmlType("Prisoner")]
    public class PrisonerId
    {
        [Required]
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
