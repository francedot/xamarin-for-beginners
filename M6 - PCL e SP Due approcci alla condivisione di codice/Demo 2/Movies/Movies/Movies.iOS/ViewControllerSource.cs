using System;
using System.Collections.Generic;
using System.Net;
using Foundation;
using UIKit;

namespace Movies.iOS
{

	public class ViewControllerSource<T> : UITableViewSource
	{
		public readonly string CellStyleName = "ViewControllerSource~" + typeof(T).Name;

		private IList<T> _dataSource;
		private readonly UITableView _tableView;

		public IList<T> DataSource
		{
			get
			{
				return _dataSource;
			}
			set
			{
				_dataSource = value;
				_tableView.ReloadData();
			}
		}
		public Func<T, string> TextFunc { get; set; }
		public Func<T, string> DetailTextFunc { get; set; }
		public Func<T, string> UriFunc { get; set; }


		public ViewControllerSource(UITableView tableView)
		{
			this._tableView = tableView;
		}

		public override nint NumberOfSections(UITableView uiTableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return DataSource.Count;
		}

		public override UITableViewCell GetCell(UITableView uiTableView, NSIndexPath indexPath)
		{
			var cell = uiTableView.DequeueReusableCell(CellStyleName);
			if (cell == null)
			{
				cell = new UITableViewCell(
					DetailTextFunc == null
						? UITableViewCellStyle.Default
						: UITableViewCellStyle.Subtitle, CellStyleName);
			}

			var item = DataSource[indexPath.Row];

			cell.TextLabel.Text = TextFunc == null ? item.ToString() : TextFunc(item);
			cell.DetailTextLabel.Text = DetailTextFunc?.Invoke(item);
			UIImage placeholderImage = UIImage.FromFile("placeholder.png");
			cell.ImageView.Image = placeholderImage;
			LoadImageAsync(cell.ImageView, new Uri(UriFunc(item)));

			return cell;
		}

		public static void LoadImageAsync(UIImageView uiImageView, Uri imageUrl)
		{
			using (var web = new WebClient())
			{
				web.DownloadDataAsync(imageUrl);
				web.DownloadDataCompleted += (sender, args) =>
				{
					uiImageView.Image = UIImage.LoadFromData(NSData.FromArray(args.Result));
				};
			}
		}
	}
}
