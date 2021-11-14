namespace SoftJail.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        static readonly string ErrorMessage = "Invalid Data";

        static string GetSuccessfullyImportedDepartmentMessage(string departmentName, int cellsCount)
            => $"Imported {departmentName} with {cellsCount} cells";

        static string GetSuccessfullyImportedPrisonerMessage(string prisonerName, int age)
            => $"Imported {prisonerName} {age} years old";

        static string GetSuccessfullyImportedOfficerMessage(string officerName, int prisonersCount)
            => $"Imported {officerName} ({prisonersCount} prisoners)";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentModels = JsonConvert.DeserializeObject<DepartmentImportModel[]>(jsonString);
            StringBuilder result = new StringBuilder();

            foreach (var departmentModel in departmentModels)
            {
                var validCells = departmentModel.Cells.Where(x => IsValid(x)).ToArray();

                if (!IsValid(departmentModel)
                    || validCells.Length != departmentModel.Cells.Length)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var department = new Department
                {
                    Name = departmentModel.Name
                };
                context.Departments.Add(department);

                foreach (var cellModel in departmentModel.Cells)
                {
                    department.Cells.Add(new Cell
                    {
                        CellNumber = cellModel.CellNumber,
                        HasWindow = cellModel.HasWindow,
                    });
                }

                result.AppendLine(
                    GetSuccessfullyImportedDepartmentMessage(
                            department.Name,
                            department.Cells.Count
                        ));
            }

            context.SaveChanges();
            return result.ToString();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var prisonersModels = JsonConvert.DeserializeObject<PrisonerImportModel[]>(jsonString);
            StringBuilder result = new StringBuilder();

            foreach (var prisonerModel in prisonersModels)
            {
                var validMailsModels = prisonerModel.Mails.Where(x => IsValid(x)).ToArray();
                if (!IsValid(prisonerModel)
                    || validMailsModels.Length != prisonerModel.Mails.Length)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                ParseDateTime(prisonerModel.IncarcerationDate, out DateTime incarcerationDate);
                ParseDateTime(prisonerModel.ReleaseDate, out DateTime releaseDate);

                if (incarcerationDate == null
                    || (!string.IsNullOrWhiteSpace(prisonerModel.ReleaseDate)
                     && releaseDate == null))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var prisoner = new Prisoner
                {
                    FullName = prisonerModel.FullName,
                    Nickname = prisonerModel.Nickname,
                    Age = prisonerModel.Age,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = releaseDate,
                    Bail = prisonerModel.Bail,
                    CellId = prisonerModel.CellId
                };
                context.Prisoners.Add(prisoner);

                foreach (var mailModel in validMailsModels)
                {
                    prisoner.Mails.Add(new Mail
                    {
                        Sender = mailModel.Sender,
                        Description = mailModel.Description,
                        Address = mailModel.Address
                    });
                }

                result.AppendLine(
                    GetSuccessfullyImportedPrisonerMessage(
                            prisoner.FullName,
                            prisoner.Age
                        ));
            }

            context.SaveChanges();

            return result.ToString();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(
                    typeof(OfficerImportModel[]),
                    new XmlRootAttribute("Officers")
                );

            var officersModels = serializer
                .Deserialize(new StringReader(xmlString)) as OfficerImportModel[];
            var result = new StringBuilder();

            foreach (var officerModel in officersModels)
            {
                if (!IsValid(officerModel))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var officer = new Officer
                {
                    FullName = officerModel.Name,
                    Salary = officerModel.Salary,
                    Position = Enum.Parse<Position>(officerModel.Position),
                    Weapon = Enum.Parse<Weapon>(officerModel.Weapon),
                    DepartmentId = officerModel.DepartmentId
                };

                context.Officers.Add(officer);
                context.SaveChanges();

                foreach (var prisoner in officerModel.Prisoners)
                {
                    context.OfficersPrisoners.Add(new OfficerPrisoner
                    {
                        Officer = officer,
                        PrisonerId = prisoner.Id
                    });
                }

                result.AppendLine(
                    GetSuccessfullyImportedOfficerMessage(
                            officer.FullName,
                            officer.OfficerPrisoners.Count
                        ));
            }

            context.SaveChanges();

            return result.ToString();
        }

        private static void ParseDateTime(string value, out DateTime into)
        {
            DateTime.TryParseExact(
                   value,
                   "dd/MM/yyyy",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out into);
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}