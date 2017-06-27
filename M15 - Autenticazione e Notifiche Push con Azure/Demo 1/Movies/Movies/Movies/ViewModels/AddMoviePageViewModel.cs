using System.Threading.Tasks;
using Movies.Models;
using Movies.Services;
using Xamarin.Forms;

namespace Movies.ViewModels
{
    public class AddMoviePageViewModel : ViewModelBase
    {
        private Movie _movie;

        public AddMoviePageViewModel()
        {
            SaveMovieCommand = new Command(async () =>
            {
                await MoviesSource.InsertMovieAsync(Movie);
                MessagingCenter.Send<AddMoviePageViewModel>(this, "RefreshMovies");
            });
        }

        public Command SaveMovieCommand { get; }

        public Movie Movie
        {
            get => _movie;
            set => Set(ref _movie, value);
        }

        public async Task OnAppearingAsync()
        {
            await Task.Yield();
            Movie = new Movie
            {
                Title = "Contoso Geeks and Where to Find Them",
                Year = "2017",
                Director = "David Yates",
                Country = "UK, USA",
                Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BMjMxOTM1OTI4MV5BMl5BanBnXkFtZTgwODE5OTYxMDI@._V1_SX300.jpg",
                Rating = 7.6,
                Genre = "Adventure, Family, Fantasy",
                Plot = "Holding a mysterious leather suitcase in his hand, Newt Scamander, a young activist wizard from England, visits New York while he is on his way to Arizona. Inside his expanding suitcase hides a wide array of diverse, magical creatures that exist among us, ranging from tiny, twig-like ones, to majestic and humongous ones. It is the middle of the 20s and times are troubled since the already fragile equilibrium of secrecy between the unseen world of wizards and the ordinary or No-Maj people that the MACUSA Congress struggles to maintain, is at risk of being unsettled. In the meantime, the voices against wizardry keep growing with daily protests led by Mary Lou Barebone and fuelled by the increasing disasters ascribed to a dark wizard, Gellert Grindelwald. At the same time, by a twist of fate, Newt's precious suitcase will be switched with the identical one of an aspiring No-Maj baker, Jacob Kowalski, while demoted Auror, Tina Goldstein, arrests Newt for being an unregistered wizard. To make matters worse, with the suitcase in the wrong hands, several creatures manage to escape to unknown directions. Before long, this situation will catch Senior Auror Percival Graves' attention who will target both Tina and Newt amid panic caused by an invisible, devastating and utterly unpredictable menace that still wreaks havoc in New York's 5th Avenue. Is there a hidden agenda behind Graves' intentions and ultimately, what will happen to the remaining magical creatures still loose in the streets?"
            };
        }
    }
}