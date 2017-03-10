using System;

using System.Linq;
using Android.App;
using Android.OS;

namespace Movies.Droid
{
	[Activity (Label = "Movies", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : ListActivity
	{
		protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            MoviesLoader.Loader = new StreamLoader(this);
            var data = await MoviesLoader.LoadMoviesAsync();
            ListAdapter = new ListAdapter<Movie>()
            {
                DataSource = data.ToList(),
                TextFunc = m => $"{m.Title}\n\n{m.Director} - {m.Country} - {m.Year}",
                UriFunc = m => m.Poster
            };
        }
	}
}


