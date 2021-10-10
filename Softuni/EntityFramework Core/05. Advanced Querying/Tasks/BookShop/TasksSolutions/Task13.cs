using System;
using System.Linq;
using BookShop.Data;

namespace BookShop.Models.TasksSolutions
{
    public static class Task13
    {
        public static string GetResult(BookShopContext context)
        {
            var books = context.Categories
                .OrderBy(x => x.Name)
                .Select(x => $"--{x.Name}{Environment.NewLine}" +
                    string.Join(Environment.NewLine, x.CategoryBooks
                        .OrderByDescending(x => x.Book.ReleaseDate)
                        .Select(x => $"{x.Book.Title} ({x.Book.ReleaseDate.Value.Year})")
                        .Take(3)))
                .ToList();

            return string.Join(Environment.NewLine, books);
        }
    }
}