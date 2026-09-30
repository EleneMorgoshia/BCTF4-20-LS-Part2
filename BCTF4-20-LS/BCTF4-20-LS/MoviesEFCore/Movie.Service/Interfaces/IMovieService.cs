using Movie.Domain.DTOs;
using Movie.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieEntity = Movie.Domain.Entities.Movie;
namespace Movie.Service.Interfaces
{
    public interface IMovieService
    {
        Task<ICollection<MovieDTO>> GetAllMoviesAsync(CancellationToken cto = default);
        Task AddMovieAsync(CreateMovieDTO movieDTO);
        Task<MovieDTO?> GetMovieById(int id);

        Task UpdateMovieAsync(int id, UpdateMovieDTO updateMovieDTO);
        Task DeleteMovieAsync(int id);

        //homework task1
        Task<ICollection<SearchMovieDTO>> GetMoviesByStudioAsync(int year, string studioName, int minimumActorCount);


        //homework task2
        Task<ICollection<SearchMovieDTO>> GetMoviesByCountryAsync(string countryName, int minimumYear, int maximumActorCount);

        //homework  task3
        Task<ICollection<SearchMovieDTO>> GetMoviesAdvancedAsync(int fromYear, int toYear, string countryName,
            string titleText, int minimumActorCount);
    }
}
