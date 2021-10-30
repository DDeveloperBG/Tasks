using System.IO;

namespace CarDealer.IO
{
    public static class Reader
    {
        public static string ReadFrom(string fileName)
        {
            return File.ReadAllText("./Datasets/" + fileName);
        }
    }
}
