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
        private readonly IUnitOfWork _unitOfWork;
        public MovieService(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
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
            await _unitOfWork.SaveChangesAsync();
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

        public async Task UpdateMovieAsync(int id, UpdateMovieDTO updateMovieDTO)
        {
            if (id <= 0)
                throw new ArgumentException("Id cannot be negative or 0");
            if(updateMovieDTO == null)
                throw new ArgumentNullException(nameof(updateMovieDTO));


            //გადაქასთვა movie entity-ში
            MovieEntity movie = new MovieEntity
            {
                Id = id,
                Title = updateMovieDTO.Title,
                ReleaseYear = updateMovieDTO.ReleaseYear,
                StudioId = updateMovieDTO.StudioId,
            };

            await _movieRepository.UpdateMovieAsync(id, movie);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task DeleteMovieAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id cannot be negative or 0");
            await _movieRepository.DeleteMovieAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }


        //homework task1
        public async Task<ICollection<SearchMovieDTO>> GetMoviesByStudioAsync(int year, string studioName, int minimumActorCount)
        {
            if (year <= 0)
                throw new ArgumentException("Year cannot be negative or 0");

            if (string.IsNullOrWhiteSpace(studioName))
                throw new ArgumentException("Studio name cannot be null or empty.", nameof(studioName));

            if (minimumActorCount < 0)
                throw new ArgumentException("Minimum actor count cannot be negative.", nameof(minimumActorCount));

            var movies = await _movieRepository.SearchMoviesByStudioAsync(year, studioName, minimumActorCount);
            var moviesDTO = movies.Select(m => MapMovieDTO(m)).ToList();
            return moviesDTO;
        }


        //homework task2
        public async Task<ICollection<SearchMovieDTO>> GetMoviesByCountryAsync(string countryName, int minimumYear, int maximumActorCount)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                throw new ArgumentException("Country name cannot be empty or null");
            if (minimumYear <= 0)
                throw new ArgumentException("Minimum year cannot be negative or 0");
            if (maximumActorCount < 0)
                throw new ArgumentException("Maximum actor count cannot be negative or 0");

            var movies = await _movieRepository.SearchMoviesByCountryAsync(countryName, minimumYear, maximumActorCount);
            var moviesDTO = movies.Select(m => MapMovieDTO(m)).ToList();
            return moviesDTO;
        }



        //homework  task3
        public async Task<ICollection<SearchMovieDTO>> GetMoviesAdvancedAsync(int fromYear, int toYear, string countryName,
            string titleText, int minimumActorCount)
        {
            if (fromYear <= 0)
                throw new ArgumentException("From year cannot be negative or 0");
            if (toYear <= 0)
                throw new ArgumentException("To year cannot be negative or 0");
            if (string.IsNullOrWhiteSpace(countryName))
                throw new ArgumentException("Country name cannot be  empty or null");

            var movies = await _movieRepository.SearchMoviesAdvancedAsync(fromYear, toYear, countryName, titleText, minimumActorCount);
            var moviesDTO = movies.Select(m => MapMovieDTO(m)).ToList();
            return moviesDTO;
        }

        private static SearchMovieDTO MapMovieDTO(MovieEntity movie)
        {
            return new SearchMovieDTO
            {
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                StudioName = movie.Studio.Name,
                CountryName = movie.Studio.Country.Name,
                ActorsCount = movie.Actors.Count
            };
        }
    }
}

