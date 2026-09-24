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
            //await _movieContext.SaveChangesAsync();

        }

        public async Task<ICollection<MovieEntity>> GetAllMoviesAsync()
        {
            return await _movieContext.Movies
                    .Include(m => m.Studio)
                    .ToListAsync();
        }

        //classwork
        public async Task<MovieEntity?> GetMovieById(int id)
        {
            var movies = await GetAllMoviesAsync();
            var movieById = movies.FirstOrDefault(m => m.Id == id);
            return movieById;

        }

        //update
        public async Task UpdateMovieAsync(int id, MovieEntity movie)
        {
            var movieById = await GetMovieById(id);
            if (movieById == null)
                throw new ArgumentException("There is no movie with this id");

            _movieContext.Movies.Update(movieById);
            //await _movieContext.SaveChangesAsync();
        }

        //delete
        public async Task DeleteMovieAsync(int id)
        {
            var movieById = await GetMovieById(id);
            if (movieById == null)
                throw new ArgumentException("There is no movie with this id");

            _movieContext.Movies.Remove(movieById);
            //await _movieContext.SaveChangesAsync();
        }
    }
}
