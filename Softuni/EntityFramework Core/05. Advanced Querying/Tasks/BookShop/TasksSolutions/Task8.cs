using System;
using System.Linq;
using BookShop.Data;

namespace BookShop.Models.TasksSolutions
{
    public static class Task8
    {
        public static string GetResult(BookShopContext context, string input)
        {
            input = input.ToLower();

            var books = context.Books
               .Where(x => x.Title.ToLower().Contains(input))
               .Select(x => x.Title)
               .OrderBy(x => x)
               .ToList();

            return string.Join(Environment.NewLine, books);
        }
    }
}