using Movie.Domain.DTOs;
using Movie.Domain.Interfaces;
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
    }
}
