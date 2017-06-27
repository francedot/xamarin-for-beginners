using Android.Speech.Tts;
using Movies.Droid.Services;
using Xamarin.Forms;
using ITextToSpeech = Movies.Services.ITextToSpeech;

[assembly: Dependency(typeof(TextToSpeechAndroid))]
namespace Movies.Droid.Services
{
    public class TextToSpeechAndroid : ITextToSpeech
    {
        private readonly TextToSpeechListener _listener;
        private static string _text;

        public TextToSpeechAndroid()
        {
            _listener = new TextToSpeechListener();
        }

        public void Speak(string text)
        {
            _text = text;
            if (_listener.Speech == null)
            {
                _listener.Speech = new TextToSpeech(Forms.Context, _listener);
            }
            else
            {
                _listener.Speech.Speak(text, QueueMode.Flush, null, null);
            }
        }

        public class TextToSpeechListener : Java.Lang.Object, TextToSpeech.IOnInitListener
        {
            private TextToSpeech _speech;

            public void OnInit(OperationResult status)
            {
                if (status == OperationResult.Success)
                {
                    _speech.Speak(_text, QueueMode.Flush, null, null);
                }
            }

            public TextToSpeech Speech
            {
                get => _speech;
                set => _speech = value;
            }
        }
    }
}