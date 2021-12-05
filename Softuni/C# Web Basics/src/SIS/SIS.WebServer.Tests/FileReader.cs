using System.IO;

namespace SIS.WebServer.Tests
{
    public static class FileReader
    {
        public static string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
