using System.Linq;
using BookShop.Data;

namespace BookShop.Models.TasksSolutions
{
    public static class Task15
    {
        public static int GetResult(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Copies < 4200)
                .ToList();

            foreach (var book in books)
            {
                context.Books.Remove(book);
            }

            context.SaveChanges();

            return books.Count;
        }
    }
}