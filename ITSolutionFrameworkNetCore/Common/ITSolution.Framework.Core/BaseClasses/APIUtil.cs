using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ITSolution.Framework.Core.Common.BaseClasses
{
    public class ApiUtil
    {
        public static HttpClient HttpClient
        {
            get
            {
                if (httpClient == null)
                {
                    httpClient = new HttpClient();

                }
                return httpClient;
            }
        }
        private static HttpClient httpClient;

        static string urlLogin;
        static string httpProt;
        static string host;


        public async static Task<List<T>> GetListFromAPI<T>(string url, HttpContext httpContext)
        {
            httpProt = httpContext.Request.Scheme;
            host = httpContext.Request.Host.ToString();
            urlLogin = string.Format("{0}://{1}/api/auth/logincookie", httpProt, host);

            string loginData = JsonConvert.SerializeObject(
                new { Email = httpContext.User.Identity.Name, RememberMe = true }
                );

            if (httpClient == null)
            {
                StringContent content = new StringContent(loginData, System.Text.Encoding.UTF8, "application/json");
                await HttpClient.PostAsync(urlLogin, content);
            }

            using (var response = await HttpClient.GetAsync(url))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(apiResponse);
            }

        }
    }
}
