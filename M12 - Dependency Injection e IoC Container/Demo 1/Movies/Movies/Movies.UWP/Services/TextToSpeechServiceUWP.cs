using System;
using Windows.UI.Xaml.Controls;
using Movies.Services;

namespace Movies.UWP.Services
{
    public class TextToSpeechServiceUWP : TextToSpeechService
    {
        public override async void Speak(string text)
        {
            var mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            var stream = await synth.SynthesizeTextToStreamAsync(text);

            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
    }
}