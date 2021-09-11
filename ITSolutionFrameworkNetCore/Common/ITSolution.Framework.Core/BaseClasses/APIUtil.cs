using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ITSolution.Framework.Core.BaseClasses
{
    public class APIUtil
    {
        public async static Task<List<T>> GetListFromAPI<T>(string url, HttpClient client)
        {
            try
            {
                using (var response = await client.GetAsync(url))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(apiResponse);
                }
            }
            catch (Exception ex)
            {
                Utils.ShowExceptionStack(ex);
                return new List<T>();
            }
        }
    }
}
