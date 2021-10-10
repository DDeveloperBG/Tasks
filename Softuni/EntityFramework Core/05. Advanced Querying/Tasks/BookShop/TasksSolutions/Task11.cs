using System;
using System.Linq;
using BookShop.Data;

namespace BookShop.Models.TasksSolutions
{
    public static class Task11
    {
        public static string GetResult(BookShopContext context)
        {
            var books = context.Authors
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    TotalBookCopies = x.Books.Sum(x => x.Copies)
                })
                .OrderByDescending(x => x.TotalBookCopies)
                .ToList();

            return string.Join(Environment.NewLine, books
                .Select(x => $"{x.FirstName} {x.LastName} - {x.TotalBookCopies}"));
        }
    }
}