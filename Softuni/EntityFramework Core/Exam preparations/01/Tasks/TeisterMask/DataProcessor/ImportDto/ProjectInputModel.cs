using System;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ProjectInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public string OpenDate { get; set; }

        public string DueDate { get; set; }

        [XmlArrayItem("Task")]
        public TaskInputModel[] Tasks { get; set; }
    }

    public class TaskInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public string OpenDate { get; set; }

        [Required]
        public string DueDate { get; set; }

        [Required]
        [EnumDataType(typeof(ExecutionType))]
        public string ExecutionType { get; set; }

        [Required]
        [EnumDataType(typeof(LabelType))]
        public string LabelType { get; set; }
    }
}
