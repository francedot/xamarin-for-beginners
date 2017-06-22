using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
using Android.Support.V4.Content.Res;
using Android.Support.V7.App;
using Android.Views;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Object = Java.Lang.Object;

namespace CognitiveFace
{
    [Activity(Label = "CognitiveFace", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        private const int PickImage = 1;
        private LinearLayout _contentLayout;
        private TextView _placeholderTextView;
        private ImageView _imageView;
        private ProgressDialog _progressDialog;
        private Bitmap _currentBitmap;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            _imageView = FindViewById<ImageView>(Resource.Id.imageView);
            _contentLayout = FindViewById<LinearLayout>(Resource.Id.contentLayout);
            _placeholderTextView = FindViewById<TextView>(Resource.Id.placeholderTextView);

            _progressDialog = new ProgressDialog(this);
            var input = Assets.Open("ErikaMustermann.jpg");
            _currentBitmap = BitmapFactory.DecodeStream(input);
            _imageView.SetImageBitmap(_currentBitmap);

            var browseImageButton = FindViewById<FloatingActionButton>(Resource.Id.browseImageButton);
            var detectButton = FindViewById<FloatingActionButton>(Resource.Id.detectButton);
            browseImageButton.Click += OnBrowseImageClick;
            detectButton.Click += OnDetectClick;
        }

        private void OnBrowseImageClick(object sender, EventArgs eventArgs)
        {
            var galleryIntent = new Intent(Intent.ActionGetContent);
            galleryIntent.SetType("image/*");
            StartActivityForResult(Intent.CreateChooser(galleryIntent, "Choose a Picture"), PickImage);
        }

        private void OnDetectClick(object sender, EventArgs e)
        {
            byte[] bitmapArray;
            using (var stream = new MemoryStream())
            {
                _currentBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                bitmapArray = stream.ToArray();
            }

            var inputStream = new MemoryStream(bitmapArray);

            var task = new DetectFaceTask(this)
            {
                ProgressDialog = _progressDialog
            };
            task.Execute(inputStream);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode != PickImage || resultCode != Result.Ok || data?.Data == null)
            {
                return;
            }

            _contentLayout.Visibility = ViewStates.Gone;
            _placeholderTextView.Visibility = ViewStates.Visible;

            var uri = data.Data;

            _currentBitmap = MediaStore.Images.Media.GetBitmap(ContentResolver, uri);
            var imageView = FindViewById<ImageView>(Resource.Id.imageView);
            imageView.SetImageBitmap(_currentBitmap);
        }

        public class DetectFaceTask : AsyncTask<Stream, string, Face[]>
        {
            private readonly FaceServiceClient _faceServiceClient =
                new FaceServiceClient("Your Face API Key", "https://westeurope.api.cognitive.microsoft.com/face/v1.0");
            private readonly Activity _activity;

            public DetectFaceTask(Activity activity)
            {
                _activity = activity;
            }

            public ProgressDialog ProgressDialog { get; set; }

            protected override Face[] RunInBackground(params Stream[] @params)
            {
                try
                {
                    PublishProgress("Detecting...");
                    var result = _faceServiceClient.DetectAsync(@params[0], true, true, GetFaceAttributeTypes()).Result;
                    if (result == null)
                    {
                        PublishProgress("Detection Finished. Nothing detected");
                        return null;
                    }
                    PublishProgress("Detection Finished");
                    return result;
                }
                catch (Exception)
                {
                    PublishProgress("Detection Failed");
                    return null;
                }
            }

            protected override void OnPreExecute()
            {
                // Show progress dialog
                ProgressDialog.Show();
            }
            protected override void OnProgressUpdate(params string[] progress)
            {
                // Update progress dialog
                ProgressDialog.SetMessage(progress[0]);
            }

            protected override void OnPostExecute(Object result)
            {
                base.OnPostExecute(result);
                // Must be called before OnPostExecute of Faces
            }

            protected override void OnPostExecute(Face[] result)
            {
                base.OnPostExecute(result);

                // Dismiss progress dialog
                ProgressDialog.Dismiss();

                var face = result?.FirstOrDefault();
                if (face == null)
                {
                    return;
                }

                var imageview = _activity.FindViewById<ImageView>(Resource.Id.imageView);
                var bitmap = ((BitmapDrawable)imageview.Drawable).Bitmap;
                imageview.SetImageBitmap(DrawFaceRectangle(bitmap, face));

                var ageTextView = _activity.FindViewById<TextView>(Resource.Id.ageTextView);
                ageTextView.Text = $"{face.FaceAttributes.Age}";

                var genderTextView = _activity.FindViewById<TextView>(Resource.Id.genderTextView);
                genderTextView.Text = $"{face.FaceAttributes.Gender}";

                var smileTextView = _activity.FindViewById<TextView>(Resource.Id.smileTextView);
                smileTextView.Text = $"{face.FaceAttributes.Smile * 100}%";

                var glassesTextView = _activity.FindViewById<TextView>(Resource.Id.glassesTextView);
                glassesTextView.Text = $"{face.FaceAttributes.Glasses}";

                var headPose = face.FaceAttributes.HeadPose;
                var headPoseTextView = _activity.FindViewById<TextView>(Resource.Id.headPoseTextView);
                headPoseTextView.Text = $"({headPose.Yaw}, {headPose.Pitch}, {headPose.Roll})";

                var angerTextView = _activity.FindViewById<TextView>(Resource.Id.angerTextView);
                angerTextView.Text = $"{face.FaceAttributes.Emotion.Anger * 100}%";

                var contemptTextView = _activity.FindViewById<TextView>(Resource.Id.contemptTextView);
                contemptTextView.Text = $"{face.FaceAttributes.Emotion.Contempt * 100}%";

                var disgustTextView = _activity.FindViewById<TextView>(Resource.Id.disgustTextView);
                disgustTextView.Text = $"{face.FaceAttributes.Emotion.Disgust * 100}%";

                var fearTextView = _activity.FindViewById<TextView>(Resource.Id.fearTextView);
                fearTextView.Text = $"{face.FaceAttributes.Emotion.Fear * 100}%";

                var happinessTextView = _activity.FindViewById<TextView>(Resource.Id.happinessTextView);
                happinessTextView.Text = $"{face.FaceAttributes.Emotion.Happiness * 100}%";

                var neutralTextView = _activity.FindViewById<TextView>(Resource.Id.neutralTextView);
                neutralTextView.Text = $"{face.FaceAttributes.Emotion.Neutral * 100}%";

                var sadnessTextView = _activity.FindViewById<TextView>(Resource.Id.sadnessTextView);
                sadnessTextView.Text = $"{face.FaceAttributes.Emotion.Sadness * 100}%";

                var surpriseTextView = _activity.FindViewById<TextView>(Resource.Id.surpriseTextView);
                surpriseTextView.Text = $"{face.FaceAttributes.Emotion.Surprise * 100}%";

                var contentLayout = _activity.FindViewById<LinearLayout>(Resource.Id.contentLayout);
                contentLayout.Visibility = ViewStates.Visible;

                var placeholderTextView = _activity.FindViewById<TextView>(Resource.Id.placeholderTextView);
                placeholderTextView.Visibility = ViewStates.Gone;
            }

            private Bitmap DrawFaceRectangle(Bitmap bitmap, Face face)
            {
                var newBitmap = bitmap.Copy(Bitmap.Config.Argb8888, true);

                if (face == null)
                {
                    return newBitmap;
                }

                var canvas = new Canvas(newBitmap);
                var paint = new Paint
                {
                    AntiAlias = true
                };
                paint.SetStyle(Paint.Style.Stroke);
                var green = ResourcesCompat.GetColor(_activity.Resources, Resource.Color.Green, null);
                paint.Color = new Color(green);
                const int strokeWidth = 2;
                paint.StrokeWidth = strokeWidth;

                var faceRectangle = face.FaceRectangle;
                canvas.DrawRect(
                    faceRectangle.Left,
                    faceRectangle.Top,
                    faceRectangle.Left + faceRectangle.Width,
                    faceRectangle.Top + faceRectangle.Height,
                    paint);

                return newBitmap;
            }

            private IEnumerable<FaceAttributeType> GetFaceAttributeTypes()
            {
                return Enum.GetValues(typeof(FaceAttributeType)).Cast<FaceAttributeType>();
            }
        }
    }
}

