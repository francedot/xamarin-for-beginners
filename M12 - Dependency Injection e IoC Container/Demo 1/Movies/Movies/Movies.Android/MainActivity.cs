using Android.App;
using Android.Content.PM;
using Android.OS;
using Movies.Droid.Services;
using Movies.Services;

namespace Movies.Droid
{
    [Activity(Label = "Movies", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // Service Injection
            TextToSpeechService.Create = () => new TextToSpeechServiceAndroid();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

