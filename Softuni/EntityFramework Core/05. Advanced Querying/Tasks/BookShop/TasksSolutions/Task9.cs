using System;
using System.Linq;
using BookShop.Data;

namespace BookShop.Models.TasksSolutions
{
    public static class Task9
    {
        public static string GetResult(BookShopContext context, string input)
        {
            input = input.ToLower();

            var books = context.Books
                .Where(x => x.Author.LastName.ToLower().StartsWith(input))
                .OrderBy(x => x.BookId)
                .Select(x => $"{x.Title} ({x.Author.FirstName} {x.Author.LastName})")
                .ToList();

            return string.Join(Environment.NewLine, books);
        }
    }
}