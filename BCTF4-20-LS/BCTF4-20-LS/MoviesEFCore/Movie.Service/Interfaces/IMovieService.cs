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
        Task<ICollection<MovieDTO>> GetAllMoviesAsync();
        Task AddMovieAsync(CreateMovieDTO movieDTO);
        Task<MovieDTO?> GetMovieById(int id);

        Task UpdateMovieAsync(int id, UpdateMovieDTO updateMovieDTO);
        Task DeleteMovieAsync(int id);
     
    }
}
