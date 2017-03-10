using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Movies
{
	public static class MoviesLoader
	{
	    private const string Filename = "movies.json";

	    public static async Task<IEnumerable<Movie>> LoadMoviesAsync()
		{
			using (var streamReader = new StreamReader(await OpenMoviesStreamAsync())) {
				return JsonConvert.DeserializeObject<List<Movie>>(await streamReader.ReadToEndAsync());
			}
		}

		private static async Task<Stream> OpenMoviesStreamAsync()
		{
#if __IOS__
			return File.OpenRead(Filename);
#elif __ANDROID__
			return Android.App.Application.Context.Assets.Open(Filename);
#else // UWP
			var sf = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(Filename);
			return await sf.OpenStreamForReadAsync();
#endif
        }
    }
}

