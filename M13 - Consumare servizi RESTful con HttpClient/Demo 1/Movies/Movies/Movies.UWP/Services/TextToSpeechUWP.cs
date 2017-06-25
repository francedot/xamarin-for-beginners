using System;
using Windows.UI.Xaml.Controls;
using Movies.Services;
using Movies.UWP.Services;

[assembly: Xamarin.Forms.Dependency(typeof(TextToSpeechUWP))]
namespace Movies.UWP.Services
{
    public class TextToSpeechUWP : ITextToSpeech
    {
        public async void Speak(string text)
        {
            var mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            var stream = await synth.SynthesizeTextToStreamAsync(text);

            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
    }
}