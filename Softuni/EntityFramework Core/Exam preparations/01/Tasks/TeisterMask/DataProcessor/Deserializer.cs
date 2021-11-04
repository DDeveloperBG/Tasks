namespace TeisterMask.DataProcessor
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    using Data;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
    using TeisterMask.DataProcessor.ImportDto;
    using System.Text;
    using System.IO;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProjectInputModel[]), new XmlRootAttribute("Projects"));

            var projectsDTOs = serializer.Deserialize(new StringReader(xmlString)) as ProjectInputModel[];
            var projects = new List<Project>();
            var result = new StringBuilder();

            foreach (var projectDTO in projectsDTOs)
            {
                if (!IsValid(projectDTO))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime projectOpenDate;
                DateTime? projectDueDate;

                try
                {
                    projectOpenDate = GetDateTime(projectDTO.OpenDate);
                    projectDueDate = string.IsNullOrEmpty(projectDTO.DueDate) ? null : GetDateTime(projectDTO.DueDate) as DateTime?;
                }
                catch (Exception)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                List<Task> tasks = new List<Task>();

                foreach (var taskDTO in projectDTO.Tasks)
                {
                    if (!IsValid(taskDTO))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    try
                    {
                        DateTime taskOpenDate = GetDateTime(taskDTO.OpenDate);
                        DateTime taskDueDate = GetDateTime(taskDTO.DueDate);

                        if (taskOpenDate < projectOpenDate
                            || taskDueDate > projectDueDate
                            || taskDueDate < taskOpenDate)
                        {
                            throw new ArgumentException(ErrorMessage);
                        }

                        Task task = new Task
                        {
                            Name = taskDTO.Name,
                            OpenDate = taskOpenDate,
                            DueDate = taskDueDate,
                            LabelType = Enum.Parse<LabelType>(taskDTO.LabelType),
                            ExecutionType = Enum.Parse<ExecutionType>(taskDTO.ExecutionType),
                        };

                        tasks.Add(task);
                    }
                    catch (ArgumentException e)
                    {
                        result.AppendLine(e.Message);
                    }
                }

                Project project = new Project
                {
                    Name = projectDTO.Name,
                    OpenDate = projectOpenDate,
                    DueDate = projectDueDate,
                    Tasks = tasks,
                };

                projects.Add(project);
                result.AppendLine(
                    string.Format(SuccessfullyImportedProject, project.Name, tasks.Count));
            }

            context.Projects.AddRange(projects);
            context.SaveChanges();

            return result.ToString().Trim();
        }

        private static DateTime GetDateTime(string value)
        {
            DateTime date;

            bool isSuccessful = DateTime.TryParseExact(value,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);

            if (!isSuccessful)
            {
                throw new ArgumentException(ErrorMessage);
            }

            return date;
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employeesDTOs = JsonConvert
                .DeserializeObject<ICollection<EmployeeInputModel>>(jsonString);

            var result = new StringBuilder();
            var employeesAndTasksIds = new Dictionary<Employee, HashSet<int>>();

            foreach (var employeeDTO in employeesDTOs)
            {
                if (!IsValid(employeeDTO))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Employee employee = new Employee
                {
                    Username = employeeDTO.Username,
                    Email = employeeDTO.Email,
                    Phone = employeeDTO.Phone,
                };

                employeesAndTasksIds.Add(employee, employeeDTO.Tasks);                
            }

            context.Employees.AddRange(employeesAndTasksIds.Keys);
            context.SaveChanges();

            var tasksIds = context.Tasks.Select(x => x.Id).ToHashSet();

            foreach (var kvp in employeesAndTasksIds)
            {
                foreach (var taskId in kvp.Value)
                {
                    if (!tasksIds.Contains(taskId))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    kvp.Key.EmployeesTasks.Add(new EmployeeTask
                    {
                        TaskId = taskId,
                        EmployeeId = kvp.Key.Id
                    });
                }

                result.AppendLine(
                    string.Format(SuccessfullyImportedEmployee, 
                        kvp.Key.Username, 
                        kvp.Key.EmployeesTasks.Count));
            }

            context.SaveChanges();

            return result.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}