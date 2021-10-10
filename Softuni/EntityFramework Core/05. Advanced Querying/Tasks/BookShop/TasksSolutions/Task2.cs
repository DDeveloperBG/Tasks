using System;
using System.Linq;
using BookShop.Data;
using BookShop.Models.Enums;

namespace BookShop.Models.TasksSolutions
{
    public static class Task2
    {
        public static string GetResult(BookShopContext context)
        {
            var goldenEnum = EditionType.Gold;

            var books = context.Books
                .Where(x => x.EditionType == goldenEnum && x.Copies < 5000)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }
    }
}