using System;
using System.Linq;
using BookShop.Data;

namespace BookShop.Models.TasksSolutions
{
    public static class Task10
    {
        public static int GetResult(BookShopContext context, int lengthCheck)
        {
            var booksCount = context.Books
                .Where(x => x.Title.Length > lengthCheck)
                .Count();

            return booksCount;
        }
    }
}