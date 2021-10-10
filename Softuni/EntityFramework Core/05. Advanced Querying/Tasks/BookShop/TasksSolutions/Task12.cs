using System;
using System.Linq;
using BookShop.Data;

namespace BookShop.Models.TasksSolutions
{
    public static class Task12
    {
        public static string GetResult(BookShopContext context)
        {
            var books = context.Categories
                .Select(x => new
                {
                    x.Name,
                    TotalProfit = x.CategoryBooks
                        .Select(x => x.Book.Copies * x.Book.Price).Sum()
                })
                .OrderByDescending(x => x.TotalProfit)
                .ThenBy(x => x.Name)
                .ToList();

            return string.Join(Environment.NewLine, books
                    .Select(x => $"{x.Name} ${x.TotalProfit:F2}"));
        }
    }
}