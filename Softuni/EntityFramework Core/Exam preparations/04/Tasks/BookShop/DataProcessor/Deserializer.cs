namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.Data.Models;
    using BookShop.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;
    using BookShop.Data.Models.Enums;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!\r\n";

        private static string SuccessfullyImportedBook(string bookName, decimal bookPrice)
            => $"Successfully imported book {bookName} for {bookPrice:F2}.\r\n";

        private static string SuccessfullyImportedAuthor(string authorName, int booksCount)
           => $"Successfully imported author - {authorName} with {booksCount} books.\r\n";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var serializer = new XmlSerializer(
                     typeof(BookImportModel[]),
                     new XmlRootAttribute("Books")
                 );

            var booksModels = serializer
                .Deserialize(new StringReader(xmlString)) as BookImportModel[];
            var result = new StringBuilder();

            foreach (var bookModel in booksModels)
            {
                if (!IsValid(bookModel))
                {
                    result.Append(ErrorMessage);
                    continue;
                }

                ParseDateTime(bookModel.PublishedOn, out DateTime publishedOn);

                if (publishedOn == null)
                {
                    result.Append(ErrorMessage);
                    continue;
                }

                var book = new Book
                {
                    Name = bookModel.Name,
                    Price = bookModel.Price,
                    Genre = Enum.Parse<Genre>(bookModel.Genre),
                    Pages = bookModel.Pages,
                    PublishedOn = publishedOn
                };

                context.Books.Add(book);

                result.Append(SuccessfullyImportedBook(book.Name, book.Price));
            }

            context.SaveChanges();

            return result.ToString();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var authorsModels = JsonConvert.DeserializeObject<AuthorImportModel[]>(jsonString);
            StringBuilder result = new StringBuilder();
            HashSet<int> booksIds = context.Books.Select(x => x.Id).ToHashSet();
            HashSet<string> emails = new HashSet<string>();

            foreach (var authorModel in authorsModels)
            {
                var validBookModels = authorModel
                    .Books
                    .Where(x => x.Id.HasValue && booksIds.Contains(x.Id.Value))
                    .Select(x => x.Id.Value)
                    .ToArray();

                if (!IsValid(authorModel)
                    || validBookModels.Length == 0
                    || emails.Contains(authorModel.Email))
                {
                    result.Append(ErrorMessage);
                    continue;
                }
                else
                {
                    emails.Add(authorModel.Email);
                }

                var author = new Author
                {
                    FirstName = authorModel.FirstName,
                    LastName = authorModel.LastName,
                    Email = authorModel.Email,
                    Phone = authorModel.Phone,
                };

                context.Authors.Add(author);

                foreach (var bookId in validBookModels)
                {
                    context.AuthorsBooks.Add(new AuthorBook
                    {
                        Author = author,
                        BookId = bookId
                    });
                }

                result.Append(
                    SuccessfullyImportedAuthor(
                        author.FirstName + " " + author.LastName,
                        author.AuthorsBooks.Count)
                    );
            }

            context.SaveChanges();

            return result.ToString();
        }

        private static void ParseDateTime(string value, out DateTime into)
        {
            DateTime.TryParseExact(
                   value,
                   "MM/dd/yyyy",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out into);
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}