using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Services
{
    public interface IMoviesSource
    {
        Task<IList<Movie>> GetMoviesAsync();
        Task<Movie> GetMovieAsync(int id);
        Task PostMoviesAsync(List<Movie> movies);
        Task PutMovieAsync(int id, Movie movie);
        Task DeleteMovieAsync(int id);
    }
}