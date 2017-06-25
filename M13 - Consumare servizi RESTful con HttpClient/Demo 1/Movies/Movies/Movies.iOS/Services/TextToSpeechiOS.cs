using AVFoundation;
using Movies.iOS.Services;
using Movies.Services;

[assembly: Xamarin.Forms.Dependency(typeof(TextToSpeechiOS))]
namespace Movies.iOS.Services
{
    public class TextToSpeechiOS : ITextToSpeech
    {
        public void Speak(string text)
        {
            var synthesizer = new AVSpeechSynthesizer();

            var utterance = new AVSpeechUtterance(text)
            {
                Rate = AVSpeechUtterance.DefaultSpeechRate,
                Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
                Volume = 0.5f,
                PitchMultiplier = 1.0f
            };

            synthesizer.SpeakUtterance(utterance);
        }
    }
}