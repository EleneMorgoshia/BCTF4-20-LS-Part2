using Moq;
using Movie.Domain.DTOs;
using Movie.Domain.Interfaces;
using Movie.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieEntity = Movie.Domain.Entities.Movie;

namespace Movie.Test.Services
{
    public class MovieServiceTest
    {
        //TODO - ეს ორი მეთოდი ვაფშე თავიდან გაარჩიე!
        [Fact]
        public async Task AddMovieAsync_ValidMovie_CallsRepo()
        {
            //ARRANGE
            var repoMock = new Mock<IMovieRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            //ეს რას შვრება საერთოდ???
            repoMock.Setup(r => r.AddMovieAsync(It.IsAny<MovieEntity>()))
            .Returns(Task.CompletedTask);

            var movieService = new MovieService(repoMock.Object, unitOfWorkMock.Object);

            var movieDTO = new CreateMovieDTO()
            {
                Title = "Test Movie",
                ReleaseYear = 2000,
                StudioId = 1
            };

            //ACT
            await movieService.AddMovieAsync(movieDTO);

            //ASSERT
            repoMock.Verify(repo => repo.AddMovieAsync(It.IsAny<Movie.Domain.Entities.Movie>()),
                Times.Once());

        }


        [Fact]
        public async Task AddMovieAsync_NotValidMovie_CallsRepo()
        {
            //ARRANGE
            var repoMock = new Mock<IMovieRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            //ეს რას შვრება საერთოდ???
            repoMock.Setup(r => r.AddMovieAsync(It.IsAny<MovieEntity>()))
            .Returns(Task.CompletedTask);

            var movieService = new MovieService(repoMock.Object, unitOfWorkMock.Object);

            var movieDTO = new CreateMovieDTO()
            {
                Title = "Test Movie",
                ReleaseYear = 2000,
                StudioId = 1

            };

            //ACT
            //await movieService.AddMovieAsync(movieDTO);
            var res = await Assert.ThrowsAsync<ArgumentException>(() => movieService.AddMovieAsync(movieDTO));


            //ASSERT
            // Equal არ მუშაობდა იმიტომ რომ მესიჯი არის  "Movie title cannot be null or empty. (Parameter 'Title')
            //Contains  მუშაობს

            Assert.Contains("Movie title cannot be null or empty", res.Message);  // TODo exception message should be diff

            repoMock.Verify(repo => repo.AddMovieAsync(It.IsAny<Movie.Domain.Entities.Movie>()),
                    Times.Never());

        }
    }
}
