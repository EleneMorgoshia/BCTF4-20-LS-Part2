using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieEntity = Movie.Domain.Entities.Movie;
namespace Movie.Domain.Interfaces
{
    public interface IMovieRepository
    {
        Task<ICollection<MovieEntity>> GetAllMoviesAsync(CancellationToken cto = default);
        Task AddMovieAsync(MovieEntity movie);

        Task<MovieEntity> GetMovieById(int id);
        Task UpdateMovieAsync(int id, MovieEntity movie);
        Task DeleteMovieAsync(int id);

        Task<ICollection<MovieEntity>> SearchMoviesByStudioAsync(int year, string studioName, int minimumActorCount);
        Task<ICollection<MovieEntity>> SearchMoviesByCountryAsync(string countryName, int minimumYear, int maximumActorCount);
        Task<ICollection<MovieEntity>> SearchMoviesAdvancedAsync(int fromYear, int toYear, string countryName, string titleText, int minimumActorCount);

    }
}
