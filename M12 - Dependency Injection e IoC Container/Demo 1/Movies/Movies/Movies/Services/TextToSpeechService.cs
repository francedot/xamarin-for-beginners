using System;

namespace Movies.Services
{
    public abstract class TextToSpeechService
    {
        // Factory Property
        public static Func<TextToSpeechService> Create { get; set; }

        // Contract for the ITextToSpeech feature
        public abstract void Speak(string text);
    }
}