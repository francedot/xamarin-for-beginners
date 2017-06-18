using Prism.Mvvm;
using Xamarin.Forms;

namespace MyReddit.Views
{
    public partial class PostsPage : ContentPage
    {
        public PostsPage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listview = sender as ListView;
            if (listview != null)
            {
                listview.SelectedItem = null;
            }
        }
    }
}
