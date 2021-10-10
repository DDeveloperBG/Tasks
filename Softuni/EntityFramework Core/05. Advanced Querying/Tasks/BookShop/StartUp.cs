namespace BookShop
{
    using Data;
    using System;
    using Initializer;
    using BookShop.Models.TasksSolutions;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            //string input = Console.ReadLine();
            //int l = int.Parse(Console.ReadLine());

            Console.WriteLine(Task15.GetResult(db));
        }

        // All methods here are used by softuni.judge to validate my code
        // ↓ ↓ ↓
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            return Task1.GetResult(context, command);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            return Task2.GetResult(context);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            return Task3.GetResult(context);
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            return Task4.GetResult(context, year);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            return Task5.GetResult(context, input);
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            return Task6.GetResult(context, date);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string command)
        {
            return Task7.GetResult(context, command);
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            return Task8.GetResult(context, input);
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            return Task9.GetResult(context, input);
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return Task10.GetResult(context, lengthCheck);
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            return Task11.GetResult(context);
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            return Task12.GetResult(context);
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            return Task13.GetResult(context);
        }

        public static void IncreasePrices(BookShopContext context)
        {
            Task14.GetResult(context);
        }

        public static int RemoveBooks(BookShopContext context)
        {
            return Task15.GetResult(context);
        }
    }
}
