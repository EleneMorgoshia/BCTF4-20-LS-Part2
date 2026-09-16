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
        Task<ICollection<MovieEntity>> GetAllMoviesAsync();
        Task AddMovieAsync(MovieEntity movie);

    }
}
