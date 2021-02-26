using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HARIA.Client.Extenssions
{
    internal static class HttpResponseMessageExtenssions
    {
        public static bool CheckResult(this HttpResponseMessage httpResponseMessage)
        {
            return httpResponseMessage.IsSuccessStatusCode
                ? true
                : CheckStatusCode(httpResponseMessage);
        }

        public static async Task<T> CheckResult<T>(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = (JsonContent)httpResponseMessage.Content;
                return await content.ReadFromJsonAsync<T>();
            }
            else
            {
                CheckStatusCode(httpResponseMessage);
                return default(T);
            }
        }

        private static bool CheckStatusCode(HttpResponseMessage httpResponseMessage)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.NoContent:
                case System.Net.HttpStatusCode.NotFound:
                case System.Net.HttpStatusCode.NotModified:
                    return false;

                case System.Net.HttpStatusCode.Forbidden:
                case System.Net.HttpStatusCode.NetworkAuthenticationRequired:
                case System.Net.HttpStatusCode.Unauthorized:
                    throw new System.UnauthorizedAccessException(httpResponseMessage.ReasonPhrase);

                default:
                    throw new System.Exception(httpResponseMessage.ReasonPhrase);
            }
        }
    }
}