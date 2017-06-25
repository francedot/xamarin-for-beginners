namespace Movies.Services
{
    public interface ITextToSpeech
    {
        // Contract for the ITextToSpeech feature
        void Speak(string text);
    }
}