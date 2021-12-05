namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theaters = context.Theatres.ToList()
                .Where(x => x.NumberOfHalls >= numbersOfHalls && x.Tickets.Count >= 20)
                .Select(x => new
                {
                    Name = x.Name,
                    Halls = x.NumberOfHalls,
                    TotalIncome = x.Tickets
                        .Where(y => y.RowNumber >= 1 && y.RowNumber <= 5)
                        .Sum(y => y.Price),
                    Tickets = x.Tickets
                        .Where(y => y.RowNumber >= 1 && y.RowNumber <= 5)
                        .Select(y => new
                        {
                            Price = decimal.Parse(y.Price.ToString("F2", CultureInfo.InvariantCulture)),
                            RowNumber = y.RowNumber
                        })
                        .OrderByDescending(y => y.Price)
                        .ToList()
                })
                .OrderByDescending(x => x.Halls)
                .ThenBy(x => x.Name)
                .ToList();

            return JsonConvert.SerializeObject(theaters, Formatting.Indented);
        }

        public static string ExportPlays(TheatreContext context, double rating)
        {
            var plays = context.Plays
                .ToList()
                .Where(x => x.Rating <= rating)
                .Select(x => new PlayExportModel
                {
                    Title = x.Title,
                    Duration = x.Duration.ToString("c", CultureInfo.InvariantCulture),
                    Rating = x.Rating == 0 ? "Premier" : x.Rating.ToString(),
                    Genre = x.Genre.ToString(),
                    Actors = x.Casts
                        .Where(y => y.IsMainCharacter)
                        .Select(y => new ActorExportModel
                        {
                            FullName = y.FullName,
                            MainCharacter = $"Plays main character in '{x.Title}'."
                        })
                        .OrderByDescending(y => y.FullName)
                        .ToArray()
                })
                .OrderBy(x => x.Title)
                .ThenByDescending(x => x.Genre)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(
                    typeof(PlayExportModel[]),
                    new XmlRootAttribute("Plays")
                );
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            StringBuilder result = new StringBuilder();

            serializer.Serialize(new StringWriter(result), plays, namespaces);

            return result.ToString();
        }
    }
}
