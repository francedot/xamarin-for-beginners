using System.Linq;
using UIKit;

namespace Movies.iOS
{
	public class MyTunesViewController : UITableViewController
	{
		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (UIDevice.CurrentDevice.CheckSystemVersion(7, 0))
			{
				TableView.ContentInset = new UIEdgeInsets(20, 0, 0, 0);
			}
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

            // LoadMoviesAsync the data
            MoviesLoader.Loader = new StreamLoader();
            var data = await MoviesLoader.LoadMoviesAsync();

			// Register the TableView's data source
			TableView.Source = new ViewControllerSource<Movie>(TableView)
			{
				DataSource = data.ToList(),
				TextFunc = m => m.Title,
				DetailTextFunc = m => $"{m.Director} - {m.Country} - {m.Year}",
				UriFunc = m => m.Poster
			};
		}
	}

}

