namespace HARIA.Domain.Helpers
{
    using System.Security.Cryptography;
    using System.Text;

    namespace HARIA.Domain.Helpers
    {
        public static class AuthHelper
        {
            public static string GetMd5Hash(string input)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    return sBuilder.ToString();
                }
            }
        }
    }
}