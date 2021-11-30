namespace SIS.HTTP.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string text)
        {
            char firstLetter = char.ToUpper(text[0]);
            string body = text[1..].ToLower();

            return firstLetter + body;
        }
    }
}
