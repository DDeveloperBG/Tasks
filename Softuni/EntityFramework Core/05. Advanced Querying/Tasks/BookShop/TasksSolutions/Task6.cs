using System;
using System.Linq;
using BookShop.Data;

namespace BookShop.Models.TasksSolutions
{
    public static class Task6
    {
        public static string GetResult(BookShopContext context, string date)
        {
            var dateData = date.Split('-').Select(int.Parse).ToArray();
            int day = dateData[0];
            int month = dateData[1];
            int year = dateData[2];

            var dateValue = new DateTime(year, month, day); 

            var books = context.Books
                 .Where(x => x.ReleaseDate.Value < dateValue)
                 .OrderByDescending(x => x.ReleaseDate)
                 .Select(x => $"{x.Title} - {x.EditionType} - ${x.Price:F2}")
                 .ToList();

            return string.Join(Environment.NewLine, books);
        }
    }
}