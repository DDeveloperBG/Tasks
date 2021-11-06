namespace VaporStore.DataProcessor
{
    using Data;
    using System;
    using System.Globalization;
    using System.Linq;
    using Newtonsoft.Json;
    using VaporStore.DataProcessor.DTOs.Export;
    using System.Xml.Serialization;
    using System.Text;
    using System.IO;
    using VaporStore.Data.Models.Enums;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var generes = context
                .Genres
                .ToArray()
                .Where(x => genreNames.Contains(x.Name))
                .Select(x => new GenreExportModel
                {
                    Id = x.Id,
                    Genre = x.Name,
                    Games = x.Games
                        .Where(y => y.Purchases.Count > 0)
                        .Select(y => new GameExportModel
                        {
                            Id = y.Id,
                            Title = y.Name,
                            Developer = y.Developer.Name,
                            Tags = string.Join(", ", y.GameTags.Select(c => c.Tag.Name)),
                            Players = y.Purchases.Count,
                        })
                        .OrderByDescending(x => x.Players)
                        .ThenBy(x => x.Id)
                        .ToArray()
                })
                .ToList()
                .OrderByDescending(x => x.Games.Sum(y => y.Players))
                .ThenBy(x => x.Id);

            return JsonConvert.SerializeObject(generes, Formatting.Indented);
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            PurchaseType purchaseType = Enum.Parse<PurchaseType>(storeType);

            var users = context
                .Users
                .Where(x => x.Cards.Any(y => y.Purchases.Count(c => c.Type == purchaseType) > 0))
                .Select(x => new UserExportModel
                {
                    Username = x.Username,
                    Purchases = x.Cards
                        .SelectMany(y =>
                            y.Purchases
                            .Where(c => c.Type == purchaseType)
                            .Select(c => new PurchaseExportModel
                            {
                                Card = y.Number,
                                Cvc = y.Cvc,
                                Date = c.Date,
                                Game = new GameExportModel2
                                {
                                    Title = c.Game.Name,
                                    Genre = c.Game.Genre.Name,
                                    Price = c.Game.Price
                                }
                            }))
                            .OrderBy(x => x.Date)
                            .ToArray()
                })
                .ToArray()
                .OrderByDescending(x => x.TotalSpent)
                .ThenBy(x => x.Username)
                .ToArray();

            var converter = new XmlSerializer(typeof(UserExportModel[]), new XmlRootAttribute("Users"));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            StringBuilder result = new StringBuilder();

            converter.Serialize(new StringWriter(result), users, namespaces);

            return result.ToString();
        }
    }
}