namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using BookShop.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var authors = context
                .Authors
                .Select(x => new
                {
                    AuthorName = x.FirstName + " " + x.LastName,
                    Books = x
                        .AuthorsBooks
                        .OrderByDescending(x => x.Book.Price)
                        .Select(y => new
                        {
                            BookName = y.Book.Name,
                            BookPrice = $"{y.Book.Price:F2}"
                        })
                        .ToArray()
                })
                .ToArray()
                .OrderByDescending(x => x.Books.Length)
                .ThenBy(x => x.AuthorName);

            return JsonConvert.SerializeObject(authors, Formatting.Indented);
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var books = context
                 .Books
                 .Where(x => x.PublishedOn < date && x.Genre == Data.Models.Enums.Genre.Science)
                 .Select(x => new BookExportModel
                 {
                     Pages = x.Pages,
                     Name = x.Name,
                     DateAsDateTime = x.PublishedOn
                 })
                 .ToArray()
                 .OrderByDescending(x => x.Pages)
                 .ThenByDescending(x => x.DateAsDateTime)
                 .Take(10)
                 .ToArray();

            XmlSerializer serializer = new XmlSerializer(
                   typeof(BookExportModel[]),
                   new XmlRootAttribute("Books")
               );
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            StringBuilder result = new StringBuilder();

            serializer.Serialize(new StringWriter(result), books, namespaces);

            return result.ToString();
        }
    }
}