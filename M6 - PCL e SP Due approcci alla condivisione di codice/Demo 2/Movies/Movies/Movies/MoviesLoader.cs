using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Movies
{
	public static class MoviesLoader
	{
	    public const string Filename = "movies.json";

        public static async Task<IEnumerable<Movie>> LoadMoviesAsync()
        {
            using (var streamReader = new StreamReader(await OpenMoviesStreamAsync()))
            {
                return JsonConvert.DeserializeObject<List<Movie>>(await streamReader.ReadToEndAsync());
            }
        }

        public static IStreamLoader Loader { get; set; }

        private static async Task<Stream> OpenMoviesStreamAsync()
        {
            return await Loader.GetStreamForFilename(Filename);
        }
	}
}

