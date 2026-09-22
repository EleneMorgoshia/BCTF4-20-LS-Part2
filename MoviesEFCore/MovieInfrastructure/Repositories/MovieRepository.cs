using Microsoft.EntityFrameworkCore;
using Movie.Domain.DTOs;
using Movie.Domain.Entities;
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

        public async Task<MovieEntity?> GetMovieByIdAsync(int id)
        {
            var movies = await GetAllMoviesAsync();
            var movieById = movies.FirstOrDefault(m => m.Id == id);
            return movieById;

        }

        public async Task UpdateMovieAsync(MovieEntity movie)
        {
            var movieById = await GetMovieByIdAsync(movie.Id);
            if (movieById == null)
                throw new ArgumentException("There is no movie with this id");

            movieById.Title = movie.Title;
            movieById.ReleaseYear = movie.ReleaseYear;
            movieById.StudioId = movie.StudioId;
            await _movieContext.SaveChangesAsync();


        }

        public async Task DeleteMovieAsync(int id)
        {
            var movieById = await GetMovieByIdAsync(id);
            if (movieById == null)
                throw new ArgumentException(nameof(movieById));

            _movieContext.Movies.Remove(movieById);
            await _movieContext.SaveChangesAsync();
        }
    }
}
