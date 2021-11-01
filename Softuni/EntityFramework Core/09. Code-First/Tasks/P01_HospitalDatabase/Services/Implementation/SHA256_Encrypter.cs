using System.Security.Cryptography;
using System.Text;

namespace P01_HospitalDatabase.Services
{
    public static class SHA256_Encrypter
    {
        public static bool CompareValues(string value, string hashedValue)
        {
            return Encrypt(value) == hashedValue;
        }

        public static string Encrypt(string value)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] bytes = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(value));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
