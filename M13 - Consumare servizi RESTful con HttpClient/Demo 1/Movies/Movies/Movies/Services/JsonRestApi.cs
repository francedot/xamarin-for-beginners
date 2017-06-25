using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Movies.Services
{
    public static class JsonRestApi
    {
        private static readonly JsonSerializerSettings SerializerSettings = 
            new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            };

        // HTTP GET Request
        public static async Task<TResult> GetAsync<TResult>(string urlString, CancellationToken cancellationToken = default(CancellationToken))
        {
            string content = null;

            var httpClient = CreateHttpClient();

            var response = await httpClient.GetAsync(urlString, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            httpClient.Dispose();

            return JsonConvert.DeserializeObject<TResult>(content);
        }

        // HTTP POST Request
        public static async Task<string> PostAsync<TRequest>(string urlString, TRequest valueRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            string result = null;

            var httpClient = CreateHttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var serializedValue = JsonConvert.SerializeObject(valueRequest, Formatting.None, SerializerSettings).EscapeForWebApi();
            var stringContent = new StringContent(serializedValue, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(urlString, stringContent, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }

        // HTTP PUT Request
        public static async Task<string> PutAsync<TRequest>(string urlString, TRequest valueRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            string result = null;

            var httpClient = CreateHttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var serializedValue = JsonConvert.SerializeObject(valueRequest, Formatting.None, SerializerSettings).EscapeForWebApi();
            var stringContent = new StringContent(serializedValue, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(urlString, stringContent, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }

        // HTTP DELETE Request
        public static async Task<string> DeleteAsync(string urlString, CancellationToken cancellationToken = default(CancellationToken))
        {
            string result = null;

            var httpClient = CreateHttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.DeleteAsync(urlString, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }

        private static HttpClient CreateHttpClient()
        {
            return new HttpClient(
                new HttpClientHandler
                {
                    AutomaticDecompression =
                        DecompressionMethods.Deflate |
                        DecompressionMethods.GZip
                });
        }
    }

    public static class StringExtensions
    {
        public static string EscapeForWebApi(this string value)
        {
            return "\'" + value.Replace("'", "\\\\'") + "\'";
        }
    }
}