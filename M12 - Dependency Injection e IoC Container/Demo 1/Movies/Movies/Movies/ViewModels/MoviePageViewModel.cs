using System.Windows.Input;
using Movies.Models;
using Movies.Services;
using Xamarin.Forms;

namespace Movies.ViewModels
{
    public class MoviePageViewModel : ViewModelBase
    {
        private Movie _movie;

        public MoviePageViewModel()
        {
            TextToSpeechCommand = new Command<string>(text =>
            {
                var speech = TextToSpeechService.Create();
                speech.Speak(text);
            });
            MessagingCenter.Subscribe<DetailPageViewModel, Movie>(this, "SelectedMovie", (model, movie) =>
            {
                Movie = movie;
            });
        }

        public ICommand TextToSpeechCommand { get; set; }

        public Movie Movie
        {
            get => _movie;
            set => Set(ref _movie, value);
        }
    }
}
