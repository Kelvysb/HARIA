using System;

namespace HARIA.Domain.Helpers
{
    public static class SessionIdHelper
    {
        public static string CreateSessionId()
        {
            var result = "";
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                result += ((char)random.Next(65, 90));
            }
            return result;
        }
    }
}