using Microsoft.EntityFrameworkCore;
using Movie.Domain.Interfaces;
using Movie.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieEntity = Movie.Domain.Entities.Movie;
namespace Movie.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _movieContext;
        public MovieRepository(MovieDbContext context) 
        { 
            _movieContext = context;
        }
        public async Task AddMovieAsync(MovieEntity movie)
        {
            await _movieContext.Movies.AddAsync(movie);
            await _movieContext.SaveChangesAsync();
        }

        public async Task<ICollection<MovieEntity>> GetAllMoviesAsync()
        {
            return await _movieContext.Movies
                    .Include(m => m.Studio)
                    .ToListAsync();
        }
    }
}
