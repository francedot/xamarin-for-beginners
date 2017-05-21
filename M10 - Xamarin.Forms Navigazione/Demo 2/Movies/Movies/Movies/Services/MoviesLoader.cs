using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Movies.Models;
using Newtonsoft.Json;

namespace Movies.Services
{
    public class MoviesLoader
    {
        private const string FileName = "movies.json";

        public async Task<IList<Movie>> LoadMoviesAsync()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream($"Movies.{FileName}"))
            using (var reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<List<Movie>>(await reader.ReadToEndAsync());
            }
        }
    }
}