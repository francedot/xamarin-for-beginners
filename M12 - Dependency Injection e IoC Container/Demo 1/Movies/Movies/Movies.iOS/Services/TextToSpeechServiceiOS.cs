using AVFoundation;
using Movies.Services;

namespace Movies.iOS.Services
{
    public class TextToSpeechServiceiOS : TextToSpeechService
    {
        public override void Speak(string text)
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