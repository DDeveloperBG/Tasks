using System;
using SoftUni.Data;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

namespace SoftUni
{
    public class StartUp
    {
        static void Main()
        {
            var context = new SoftUniContext();
            var result = RemoveTown(context);
            Console.WriteLine(result);
        }

        // Task 3
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            const string pattern = "{0} {1} {2} {3} {4:F2}";
            var data = context.Employees
                .OrderBy(x => x.EmployeeId)
                .Select(x =>
                    string.Format(pattern,
                        x.FirstName,
                        x.LastName,
                        x.MiddleName,
                        x.JobTitle,
                        x.Salary));

            return string.Join(Environment.NewLine, data);
        }

        // Task 4
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            const string pattern = "{0} - {1:F2}";
            var data = context.Employees
                .Where(x => x.Salary > 50000)
                .OrderBy(x => x.FirstName)
                .Select(x => string.Format(pattern, x.FirstName, x.Salary));

            return string.Join(Environment.NewLine, data);
        }

        //Task 5
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            const string wantedDepartment = "Research and Development";
            const string pattern = "{0} {1} from {2} - ${3:F2}";
            var data = context.Employees
                .Where(x => x.Department.Name == wantedDepartment)
                .OrderBy(x => x.Salary)
                .ThenByDescending(x => x.FirstName)
                .Select(x => string.Format(pattern, x.FirstName, x.LastName, x.Department.Name, x.Salary));

            return string.Join(Environment.NewLine, data);
        }

        //Task 6
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            const string newAddressText = "Vitoshka 15";
            const int newAddressTownId = 4;

            var newAddress = context.Addresses.Add(new Models.Address
            {
                AddressText = newAddressText,
                TownId = newAddressTownId
            });

            const string criteriaLastName = "Nakov";

            // Set Employee new address
            context.Employees
                .Where(x => x.LastName == criteriaLastName)
                .First().Address = newAddress.Entity;

            context.SaveChanges();

            var resultData = context.Employees
                .OrderByDescending(x => x.Address.AddressId)
                .Take(10)
                .Select(x => x.Address.AddressText);

            return string.Join(Environment.NewLine, resultData);
        }

        //Task 7
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var data = context.Employees
                .Where(x => x.EmployeesProjects
                    .Any(x => x.Project.StartDate.Year > 2000 && x.Project.StartDate.Year < 2004))
                .Take(10)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    ManagerFirstName = x.Manager.FirstName,
                    ManagerLastName = x.Manager.LastName,
                    EmployeesProjects = x.EmployeesProjects.Select(x => x.Project)
                });

            const string dateTimeFormat = "M/d/yyyy h:mm:ss tt";
            StringBuilder result = new StringBuilder();

            foreach (var employee in data)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");

                foreach (var project in employee.EmployeesProjects)
                {
                    var startDateToFormat = project.StartDate.ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                    var endDateToFormat = project.EndDate == null ? "not finished" : project.EndDate.Value.ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                    result.AppendLine($"--{project.Name} - {startDateToFormat} - {endDateToFormat}");
                }
            }

            return result.ToString().Trim();
        }

        //Task 8
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var data = context.Addresses
                .OrderByDescending(x => x.Employees.Count)
                .ThenBy(x => x.Town.Name)
                .ThenBy(x => x.AddressText)
                .Take(10)
                .Select(x => $"{x.AddressText}, {x.Town.Name} - {x.Employees.Count} employees");

            return string.Join(Environment.NewLine, data);
        }

        //Task 9
        public static string GetEmployee147(SoftUniContext context)
        {
            const int wantedEmployeeId = 147;
            var employee = context.Employees
                .First(x => x.EmployeeId == wantedEmployeeId);
            var employeeProjects = employee.EmployeesProjects
                .Select(x => x.Project.Name)
                .OrderBy(x => x);

            StringBuilder result = new StringBuilder();
            result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
            result.Append(string.Join(Environment.NewLine, employeeProjects));

            return result.ToString();
        }

        //Task 10
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var data = context.Departments
                .Where(x => x.Employees.Count > 5)
                .OrderBy(x => x.Employees.Count)
                .ThenBy(x => x.Name)
                .Select(x => $"{x.Name} - {x.Manager.FirstName} {x.Manager.LastName}\n"
                    + string.Join(Environment.NewLine, x.Employees
                            .OrderBy(x => x.FirstName)
                            .ThenBy(x => x.LastName)
                            .Select(x => $"{x.FirstName} {x.LastName} - {x.JobTitle}")));

            return string.Join(Environment.NewLine, data);
        }

        //Task 11
        public static string GetLatestProjects(SoftUniContext context)
        {
            const string dateFormat = "M/d/yyyy h:mm:ss tt";
            var data = context.Projects
                 .OrderByDescending(x => x.StartDate)
                 .Take(10)
                 .OrderBy(x => x.Name)
                 .Select(x =>
                    $"{x.Name}{Environment.NewLine}" +
                    $"{x.Description}{Environment.NewLine}" +
                    $"{x.StartDate.ToString(dateFormat, CultureInfo.InvariantCulture)}");

            return string.Join(Environment.NewLine, data);
        }

        //Task 12
        public static string IncreaseSalaries(SoftUniContext context)
        {
            HashSet<string> wantedDepartments = new HashSet<string>()
            {
                "Engineering",
                "Tool Design",
                "Marketing",
                "Information Services"
            };

            var employees = context.Employees
                .Where(x => wantedDepartments.Contains(x.Department.Name))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            employees.ForEach(x => x.Salary *= 1.12M);
            context.SaveChanges();

            return string.Join(Environment.NewLine, employees
                    .Select(x => $"{x.FirstName} {x.LastName} (${x.Salary:F2})"));
        }

        //Task 13
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            const string wantedEmployeeCriteria = "Sa";
            var data = context.Employees
                .Where(x => x.FirstName.StartsWith(wantedEmployeeCriteria))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Select(x => $"{x.FirstName} {x.LastName} - {x.JobTitle} - (${x.Salary:F2})");

            return string.Join(Environment.NewLine, data);
        }

        //Task 14
        public static string DeleteProjectById(SoftUniContext context)
        {
            const int wantedProjectId = 2;

            context.EmployeesProjects
                .Where(x => x.ProjectId == wantedProjectId)
                .ToList()
                .ForEach(x => context.EmployeesProjects.Remove(x));

            context.Projects.Remove(context.Projects.Find(wantedProjectId));

            context.SaveChanges();

            var data = context.Projects
                .Take(10)
                .Select(x => x.Name);

            return string.Join(Environment.NewLine, data);
        }

        //Task 15
        public static string RemoveTown(SoftUniContext context)
        {
            const string wantedTownName = "Seattle";
            var wantedTown = context.Towns.First(x => x.Name == wantedTownName);
            int wantedTownId = wantedTown.TownId;

            context.Employees
                .Where(x => x.Address.TownId == wantedTownId)
                .ToList()
                .ForEach(x => x.Address = null);

            var addresses = context.Addresses
                .Where(x => x.TownId == wantedTownId)
                .ToList();
            int deletedAddressesCount = addresses.Count;

            addresses.ForEach(x => context.Remove(x));

            context.Towns
                .Remove(wantedTown);

            context.SaveChanges();

            return $"{deletedAddressesCount} addresses in Seattle were deleted";
        }
    }
}