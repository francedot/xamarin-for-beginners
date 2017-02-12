using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace GroceryList
{
    [Activity(Label = "Add Item")]
    public class AddItemActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddItem);

            FindViewById<Button>(Resource.Id.saveButton).Click += OnSaveClick;
        }

        void OnSaveClick(object sender, EventArgs e)
        {
            string name = FindViewById<EditText>(Resource.Id.nameInput).Text;
            int count = int.Parse(FindViewById<EditText>(Resource.Id.countInput).Text);
        }
    }
}