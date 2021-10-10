using System;
using System.Collections.Generic;
using System.Linq;
using BookShop.Data;

namespace BookShop.Models.TasksSolutions
{
    public static class Task5
    {
        public static string GetResult(BookShopContext context, string input)
        {
            HashSet<string> categories = new HashSet<string>(
                input
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.ToLower()));

            var books = context.Books
                .Where(x => x.BookCategories
                    .Any(x => categories
                        .Contains(x.Category.Name.ToLower())))
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }
    }
}