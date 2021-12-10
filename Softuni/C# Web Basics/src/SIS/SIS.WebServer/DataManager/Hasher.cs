using System.Security.Cryptography;
using System.Text;

namespace SIS.WebServer.DataManager
{
    public static class Hasher
    {
        public static string HashValue(string value)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                return GetHashAsString(mySHA256.ComputeHash(GetValueBytes(value)));
            }
        }

        private static byte[] GetValueBytes(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        private static string GetHashAsString(byte[] hash)
        {
            return Encoding.UTF8.GetString(hash);
        }
    }
}
