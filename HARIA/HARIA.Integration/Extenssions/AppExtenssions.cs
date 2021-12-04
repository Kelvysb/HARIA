using System.Net;

namespace HARIA.Integration.Extenssions
{
    public static class AppExtenssions
    {
        public static bool ValidateApiKey(this WebApplication app, HttpContext http,  string apiKey)
        {
            if (!apiKey.Equals(app.Configuration["INTEGRATION_API_KEY"]))
            {
                http.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return false;
            }
            return true;
        }

    }
}
