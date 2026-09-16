using Movie.Domain.DTOs;
using Movie.Domain.Interfaces;
using Movie.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieEntity = Movie.Domain.Entities.Movie;
namespace Movie.Service.Implentations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        public async Task<ICollection<MovieDTO>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();
            var movieDtos = movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                ReleaseYear = m.ReleaseYear,
                StudioName = m.Studio.Name

            }).ToList();
            

            return movieDtos;
        }
        
        public async Task AddMovieAsync(CreateMovieDTO movieDTO)
        {
            if(movieDTO == null)
                throw new ArgumentNullException(nameof(movieDTO));

            if (movieDTO.Title == null)
                throw new ArgumentException("Movie title cannot be emtpy");

            if (movieDTO.StudioId <= 0)
                throw new ArgumentException("Studio id cannot be negtive or  0");

            if (movieDTO.ReleaseYear < 0)
                throw new ArgumentException("ReleaseYear cannot be less than 0");

            if (movieDTO.ReleaseYear > DateTime.Now.Year)
                throw new ArgumentException("ReleaseYear cannot be in the future");
                
            var movie = new MovieEntity
            {
                Title = movieDTO.Title,
                ReleaseYear = movieDTO.ReleaseYear,
                StudioId = movieDTO.StudioId
            };

            await _movieRepository.AddMovieAsync(movie);
        }

        public async Task<MovieDTO?> GetMovieById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Movie id cannot be negative or 0");
        
            var movieById = await _movieRepository.GetMovieById(id);
            if (movieById == null)
                throw new ArgumentException(nameof(movieById));

            MovieDTO movieDTO = new MovieDTO
            {
                Id = movieById.Id,
                Title = movieById.Title,
                ReleaseYear = movieById.ReleaseYear,
                StudioName = movieById.Studio.Name
            };

            return movieDTO;
        }
    }
}

