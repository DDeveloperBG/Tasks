using System;
using System.Linq;
using BookShop.Data;
using BookShop.Models.Enums;

namespace BookShop.Models.TasksSolutions
{
    public static class Task1
    {
        public static string GetResult(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(x => x.AgeRestriction == ageRestriction)
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }
    }
}