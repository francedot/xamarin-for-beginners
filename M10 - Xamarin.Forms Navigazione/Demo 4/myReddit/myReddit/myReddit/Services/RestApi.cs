using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyReddit.Services
{
    public class RestApi
    {
        private static RestApi _instance;

        private RestApi()
        {
        }

        public static RestApi Instance => _instance ?? (_instance = new RestApi());


        // HTTP GET Request
        public async Task<string> GetRequestAsync(string urlString, CancellationToken cancellationToken = default(CancellationToken))
        {
            string content = null;

            using (var httpClient =
                    new HttpClient(
                        new HttpClientHandler
                        {
                            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                        }))
            {
                var response = await httpClient.GetAsync(urlString, cancellationToken);
                Debug.WriteLine("Response Is" + response);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content Is" + content);
                }
            }

            return content;
        }

        // HTTP POST Request
        public async Task<string> PostRequestAsync(string urlString, string content = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            using (var httpClient =
                    new HttpClient(
                        new HttpClientHandler
                        {
                            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                        }))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(urlString, stringContent, cancellationToken);
                Debug.WriteLine("Response Is" + response);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content Is" + content);
                }

            }

            return content;
        }
    }
}