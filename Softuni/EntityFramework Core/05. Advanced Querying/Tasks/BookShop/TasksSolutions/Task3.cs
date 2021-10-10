using System;
using System.Linq;
using BookShop.Data;

namespace BookShop.Models.TasksSolutions
{
    public static class Task3
    {
        public static string GetResult(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Price > 40)
                .OrderByDescending(x => x.Price)
                .Select(x => $"{x.Title} - ${x.Price:F2}")
                .ToList();

            return string.Join(Environment.NewLine, books);
        }
    }
}