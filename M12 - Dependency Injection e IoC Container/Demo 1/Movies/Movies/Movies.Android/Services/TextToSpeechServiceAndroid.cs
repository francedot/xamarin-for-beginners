using Android.Speech.Tts;
using Xamarin.Forms;
using TextToSpeechService = Movies.Services.TextToSpeechService;

namespace Movies.Droid.Services
{
    public class TextToSpeechServiceAndroid : TextToSpeechService
    {
        private readonly TextToSpeechListener _listener;
        private static string _text;

        public TextToSpeechServiceAndroid()
        {
            _listener = new TextToSpeechListener();
        }

        public override void Speak(string text)
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