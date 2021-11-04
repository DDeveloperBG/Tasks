using System.Collections.Generic;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ProjectOutputModel
    {
        [XmlAttribute("TasksCount")]
        public int TasksCount { get => Tasks.Length; set { } }

        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }

        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public TaskOuputModel[] Tasks { get; set; }
    }

    [XmlType("Task")]
    public class TaskOuputModel
    {
        public string Name { get; set; }

        public string Label { get; set; }
    }
}
