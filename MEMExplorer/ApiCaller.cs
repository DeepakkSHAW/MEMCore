using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MEMExplorer
{
    public static class ApiCaller
    {
        // String connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        private static readonly string apiRootUri = ConfigurationManager.AppSettings["RootUrl"];
        private static readonly string apiVersion = ConfigurationManager.AppSettings["version"];
        private static readonly string apiUserid = ConfigurationManager.AppSettings["userId"];
        private static readonly string apiKey = ConfigurationManager.AppSettings["Key"];
        private static Uri GetBaseUri() {
            var newBaseUri = new Uri(new Uri(apiRootUri), apiVersion);

            if (!newBaseUri.Segments.Last().EndsWith("/"))
                newBaseUri = new Uri(newBaseUri.ToString() +"/");
            return newBaseUri;
        }
        public static async Task<Uri> Post<T>(string url, T contentValue)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(apiBasicUri);
                client.BaseAddress = GetBaseUri();
                var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                result.EnsureSuccessStatusCode();
                return result.Headers.Location;
                //return int.Parse(result.Headers.Location.ToString().Split('/').Last());
            }
        }

        public static async Task Put<T>(string url, T stringValue)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = GetBaseUri();
                var content = new StringContent(JsonConvert.SerializeObject(stringValue), Encoding.UTF8, "application/json");
                var result = await client.PutAsync(url, content);
                result.EnsureSuccessStatusCode();
            }
        }

        public static async Task<T> Get<T>(string url)
        {
            //using (var handler = new HttpClientHandler())
            //using (var client = new HttpClient(handler))
            using (var client = new HttpClient())
            {
                client.BaseAddress = GetBaseUri();
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }

        public static async Task Delete(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = GetBaseUri();
                var result = await client.DeleteAsync(url);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}