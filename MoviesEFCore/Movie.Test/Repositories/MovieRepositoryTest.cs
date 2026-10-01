using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities;
using Movie.Infrastructure.Data;
using Movie.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieEntity = Movie.Domain.Entities.Movie;
namespace Movie.Test.Repositories
{
    public class MovieRepositoryTest
    {

        //ეს მეთოდი არის დამხმარე მეთოდი, რომელიც ქმნის movieDbContext-ს,რომელიც
        //რეალურ სქლ სერვერს კი არა , არამედ მეხსიერებაში შექმნილ სატესტო ბაზას იყენებს
        private MovieDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //ეუბნება EF Core-ს: ამ ტესტისთვის ბაზა მეხსიერებაში შექმენი.
                .Options;//იღებს უკვე აწყობილ პარამეტრებს.
            return new MovieDbContext(options);
        }


        //სატესტო მეთოდის სახელი - რას ვტესტავ და რა შედეგს ველოდები 
        [Fact]
        public async Task GetAllMoviesAsync_RetrunAllMovies()
        {
            //arrange
            var context = CreateContext(); //ცარიელ სატესტრო ბაზასთად დამაკავშირებელი კონტექსტი

            //იმისთვის რომ ფილმებდამე მივიდეთ გვინდა Country - Studio - Movie
            var country = new Country { Id = 1, Name = "USA" };
            var studio = new Studio { Id = 1, Name = "Warner Brods", CountryId = country.Id, Country = country };

            //ვამატებთ ქანთრის და სტუდიოს ცალკეულად ჯერ 
            context.Countries.Add(country);
            context.Studios.Add(studio);

            var movie1 = new MovieEntity { Id = 1, Title = "Home Alone 99", ReleaseYear = 2010, StudioId = studio.Id };
            var movie2 = new MovieEntity { Id = 2, Title = "Inception2", ReleaseYear = 2008, StudioId = studio.Id };

            //ვამატებთ ფილმებს რეინჯით
            context.Movies.AddRange(movie1, movie2);
            await context.SaveChangesAsync();

            //sut - System Under Test ობიექტი.
            //ვქმნით რეპოზიტორიის ობიექტს და ვატანთ ჩვენს სატესტო ბაზას
            var sut = new MovieRepository(context);

            //act
            var result = await sut.GetAllMoviesAsync();

            //assert - რა შედეგი უნდა მქონდეს
            Assert.Equal(2, result.Count);
            Assert.Contains(result, m => m.Title == "Home Alone 99" && m.ReleaseYear == 2010);

            var singleFilm = result.First(m => m.Title == "Home Alone 99");
            Assert.Equal(1, singleFilm.Id);

            Assert.NotNull(singleFilm.Studio);
            Assert.Equal(1, singleFilm.Studio.Id);
            Assert.Equal(1, singleFilm.Studio.CountryId);
            Assert.Equal("USA", singleFilm.Studio.Country.Name);
        }

        [Fact]
        public async Task AddMovieAsync_MovieAdds()
        {
            var token = new CancellationTokenSource();



            //Arrange
            var context = CreateContext();
            var repository = new MovieRepository(context);

            var movie = new MovieEntity { Title = "Home Alone 100", ReleaseYear = 2010, StudioId = 1 };

            //act
            await repository.AddMovieAsync(movie);
            await context.SaveChangesAsync(); // ამას ვატანთ token-ს. ანუ სადაც ასინქებია მანდ


            //Assrt
            var result = await context.Movies.FirstOrDefaultAsync();  // ამას ვატანთ token-ს. ანუ სადაც ასინქებია მანდ
            Assert.NotNull(result);
            Assert.Equal("Home Alone 100", result.Title);
            Assert.Equal(2010, result.ReleaseYear);
            Assert.Equal(1, result.StudioId);

        }
    }
}
