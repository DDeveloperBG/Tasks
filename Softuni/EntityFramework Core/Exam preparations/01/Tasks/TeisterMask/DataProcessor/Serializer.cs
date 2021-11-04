namespace TeisterMask.DataProcessor
{
    using Data;
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;
    using System.Text;
    using System.IO;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context
                .Projects
                .Where(x => x.Tasks.Count > 0)
                .Select(x => new ProjectOutputModel
                {
                    ProjectName = x.Name,
                    HasEndDate = x.DueDate.HasValue ? "Yes" : "No",
                    Tasks = x.Tasks
                        .Select(y => new TaskOuputModel
                        {
                            Name = y.Name,
                            Label = y.LabelType.ToString()
                        })
                        .OrderBy(x => x.Name)
                        .ToArray()
                })
                .ToList()
                .OrderByDescending(x => x.TasksCount)
                .ThenBy(x => x.ProjectName)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(
                typeof(ProjectOutputModel[]), 
                new XmlRootAttribute("Projects"));

            StringBuilder result = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            serializer.Serialize(new StringWriter(result), projects, namespaces);

            return result.ToString();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context
                .Employees
                .Where(x => x.EmployeesTasks.Any(x => x.Task.OpenDate >= date))
                .Select(x => new
                {
                    Username = x.Username,
                    Tasks = x.EmployeesTasks
                        .Where(y => y.Task.OpenDate >= date)
                        .Select(y => new
                        {
                            TaskName = y.Task.Name,
                            OpenDate = y.Task.OpenDate,
                            DueDate = y.Task.DueDate,
                            LabelType = y.Task.LabelType.ToString(),
                            ExecutionType = y.Task.ExecutionType.ToString(),
                        })
                        .OrderByDescending(x => x.DueDate)
                        .ThenBy(x => x.TaskName)
                        .ToList()
                })
                .ToList()
                .OrderByDescending(x => x.Tasks.Count)
                .ThenBy(x => x.Username)
                .Take(10)
                .ToList();

            var settings = new JsonSerializerSettings
            {
                DateFormatString = "d",
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(employees, settings);
        }
    }
}