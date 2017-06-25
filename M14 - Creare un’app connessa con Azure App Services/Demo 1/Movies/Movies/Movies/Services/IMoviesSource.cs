using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Services
{
    public interface IMoviesSource
    {
        Task<IList<Movie>> GetMoviesAsync();
        Task<Movie> GetMovieAsync(string id);
        Task UpdateMoviesAsync(List<Movie> movies);
        Task InsertMovieAsync(Movie movie, string id = null);
        Task RemoveMovieAsync(string id);
    }
}