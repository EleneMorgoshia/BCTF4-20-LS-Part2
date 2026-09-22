using Movie.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Interfaces
{
    public interface IMovieService
    {
       Task<ICollection<MovieDTO>> GetAllMoviesAsync();

       Task AddMovieAsync(CreateMovieDTO movieDTO);

       Task<MovieDTO?> GetMovieById(int id);


       Task UpdateMovieAsync(UpdateMovieDTO movie);
       Task DeleteMovieAsync(int id);
    }
}
