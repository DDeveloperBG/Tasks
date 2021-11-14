namespace SoftJail.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context
                 .Prisoners
                 .Where(x => ids.Contains(x.Id))
                 .Select(x => new
                 {
                     Id = x.Id,
                     Name = x.FullName,
                     CellNumber = x.Cell.CellNumber,
                     Officers = x.PrisonerOfficers
                         .Select(y => new
                         {
                             OfficerName = y.Officer.FullName,
                             Department = y.Officer.Department.Name
                         })
                         .OrderBy(x => x.OfficerName)
                         .ToArray(),
                     TotalOfficerSalary = Math.Round(x.PrisonerOfficers
                        .Sum(x => x.Officer.Salary), 2)
                 })
                 .OrderBy(x => x.Name)
                 .ThenBy(x => x.Id)
                 .ToArray();

            return JsonConvert.SerializeObject(prisoners, Formatting.Indented);
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var prisonersNamesArray = prisonersNames.Split(',').ToArray();

            var prisoners = context
                .Prisoners
                .Where(x => prisonersNamesArray.Contains(x.FullName))
                .Select(x => new PrisonerExportModel
                {
                    Id = x.Id,
                    Name = x.FullName,
                    IncarcerationDateAsDate = x.IncarcerationDate,
                    EncryptedMessages = x.Mails
                        .Select(y => new Message
                        {
                            Description = y.Description
                        })
                        .ToArray()
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(
                    typeof(PrisonerExportModel[]), 
                    new XmlRootAttribute("Prisoners")
                );
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            StringBuilder result = new StringBuilder();

            serializer.Serialize(new StringWriter(result), prisoners, namespaces);

            return result.ToString();
        }
    }
}