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

        public async Task<ICollection<MovieEntity>> GetAllMoviesAsync(CancellationToken cto = default)
        {
            return await _movieContext.Movies
                    .Include(m => m.Studio)
                    .ToListAsync(cto);
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

        //homework task1
        public async Task<ICollection<MovieEntity>> SearchMoviesByStudioAsync(int year, string studioName, int minimumActorCount)
        {
            return await _movieContext.Movies
                .Include(m => m.Studio)
                    .ThenInclude(s => s.Country)
                .Include(m => m.Actors)
                .Where(m => m.ReleaseYear >= year && m.Studio.Name == studioName && m.Actors.Count >= minimumActorCount)
                .OrderByDescending(m => m.ReleaseYear)
                .ThenBy(m => m.Title)
                .ToListAsync();


            //დალაგებულში შიგნით რო დაალაგოს ვიყენებ thanBy
            //თუ მიდნა რომ მარტო რაღაც კონრეტულის მიხედვით დაალაგოს მინდა orderby
            //IQueryable -ზე ხდება აქ მუშაობა(ბაზის მხარეს, ბაზის მემორიში) და მერე როცა გავფილტრავ
            //ToListAsyncით შემომაქვს ჩემს მემორიში
        }

        //homework task2
        public async Task<ICollection<Domain.Entities.Movie>> SearchMoviesByCountryAsync(string countryName, int minimumYear, int maximumActorCount)
        {
            return await _movieContext.Movies
                .Include(m => m.Studio)
                    .ThenInclude(s => s.Country)
                .Include(m => m.Actors)
                .Where(m => m.Studio.Country.Name == countryName
                    && m.ReleaseYear >= minimumYear
                    && m.Actors.Count <= maximumActorCount)
                .OrderBy(m => m.Actors.Count)
                .ThenByDescending(m => m.ReleaseYear)
                .ThenBy(m => m.Title)
                .ToListAsync();
        }


        //homework  task3
        public async Task<ICollection<MovieEntity>> SearchMoviesAdvancedAsync(int fromYear, int toYear, string countryName,
            string titleText, int minimumActorCount)
        {
            return await _movieContext.Movies
                .Include(m => m.Studio)
                    .ThenInclude(s => s.Country)
                .Include(m => m.Actors)

                .Where(m => m.ReleaseYear >= fromYear && m.ReleaseYear <= toYear
                        && m.Studio.Country.Name == countryName
                        && m.Title.Contains(titleText)
                        && m.Actors.Count >= minimumActorCount)
                .OrderByDescending(m => m.Actors.Count)
                .ThenByDescending(m => m.ReleaseYear)
                .ThenBy(m => m.Studio.Name)
                .ThenBy(m => m.Title)
                .ToListAsync();

        }
    }
}
