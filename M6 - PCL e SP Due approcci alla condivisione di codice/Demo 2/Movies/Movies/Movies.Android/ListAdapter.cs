using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace Movies.Droid
{
	public class ListAdapter<T> : BaseAdapter<T>
	{
		private IList<T> _dataSource;

        public IList<T> DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
                this.NotifyDataSetChanged();
            }
        }

        public override int Count => _dataSource.Count;
        public override T this[int index] => _dataSource[index];

        public Func<T, string> TextFunc { get; set; }
        public Func<T, string> UriFunc { get; set; }

        public override long GetItemId(int position)
		{
			return position;
		}

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var context = Application.Context;

            var view = convertView ?? LayoutInflater.FromContext(context).Inflate(Android.Resource.Layout.ActivityListItem, null);

            var item = _dataSource[position];
            var text = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            text.Text = TextFunc == null ? item.ToString() : TextFunc(item);

            var imageView = view.FindViewById<ImageView>(Android.Resource.Id.Icon);
            LoadImageAsync(imageView, new Uri(UriFunc(item)));

            return view;
        }

	    private static void LoadImageAsync(ImageView imageView, Uri url)
	    {
	        using (var web = new WebClient())
	        {
                web.DownloadDataAsync(url);
                web.DownloadDataCompleted += (sender, args) =>
                {
                    using (var memoryStream = new MemoryStream(args.Result))
                    {
                        var icon = BitmapFactory.DecodeStream(memoryStream);
                        var n = Bitmap.CreateScaledBitmap(icon, 200, 250, true);
                        imageView.LayoutParameters.Width = 200;
                        imageView.LayoutParameters.Height = 250;
                        imageView.SetImageBitmap(n);
                    }
                };
            }
        }
    }
}

