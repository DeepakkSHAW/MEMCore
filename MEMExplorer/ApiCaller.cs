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
        // In my case this is https://localhost:44366/
        private static readonly string apiBasicUri = string.Empty;// ConfigurationManager.AppSettings["baseurl"];
        private static readonly string apiRootUri = ConfigurationManager.AppSettings["RootUrl"];
        private static readonly string apiVersion = ConfigurationManager.AppSettings["version"];
        private static readonly string apiUserid = ConfigurationManager.AppSettings["userId"];
        private static readonly string apiKey = ConfigurationManager.AppSettings["Key"];
        private static Uri getBaseUri() {
            var newBaseUri = new Uri(new Uri(apiRootUri), apiVersion);

            if (!newBaseUri.Segments.Last().EndsWith("/"))
                newBaseUri = new Uri(newBaseUri.ToString() +"/");
            return newBaseUri;
        }

        // String connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        public static async Task Post<T>(string url, T contentValue)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(apiBasicUri);
                client.BaseAddress = getBaseUri();
                var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                result.EnsureSuccessStatusCode();
                // return URI of the created resource.
                //T resultHeaderLocation = result.Headers.Location;
                //return resultHeaderLocation;
            }
        }

        public static async Task Put<T>(string url, T stringValue)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = getBaseUri();
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
                var u = getBaseUri();
                //client.BaseAddress = new Uri(apiBasicUri);
                // client.BaseAddress = new Uri("https://localhost:44350/api/v1.1/MEM/ExpCategory?IsSorted=true");
                //https://localhost:44350/api/v1.1/MEM/ExpCategory?IsSorted=true
                // client.BaseAddress = new Uri("https://localhost:44350/");
                client.BaseAddress = getBaseUri();
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
                client.BaseAddress = getBaseUri();
                var result = await client.DeleteAsync(url);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}