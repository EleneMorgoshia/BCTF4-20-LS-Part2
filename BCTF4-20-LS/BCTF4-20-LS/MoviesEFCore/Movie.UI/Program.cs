using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Domain.DTOs;
using Movie.Domain.Entities;
using Movie.Domain.Interfaces;
using Movie.Infrastructure.Data;
using Movie.Infrastructure.Repositories;
using Movie.Service.Implentations;
using Movie.Service.Interfaces;


namespace Movie.UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var _connectionString = configuration.GetConnectionString("DefaultConnection");
            #region without DI container
            //MovieDbContext movieDbContext = new MovieDbContext(_connectionString);
            //IMovieRepository movieRepository = new MovieRepository(movieDbContext);
            //IMovieService movieService = new MovieService(movieRepository);
            #endregion


            //DI container
            var services = new ServiceCollection();
            services.AddDbContext<MovieDbContext>();                     // addContext-ით ვამატებ dbContextს
            services.AddScoped<IMovieRepository, MovieRepository>();           // addScoped ით ვამათბ სერვისებს
            services.AddScoped<IMovieService, MovieService>();
            var serviceProcider = services.BuildServiceProvider();
            
            var movieService = serviceProcider.GetRequiredService<IMovieService>();
            
            
            
            
            
            
            //creating studio in a bad way:D
            //Studio newStudio = new Studio{Name = "Warner Bros", CountryId = 1 };
            //movieDbContext.Studios.Add(newStudio);
            //await movieDbContext.SaveChangesAsync();

            //addoing movie
            //CreateMovieDTO movieDTO = new CreateMovieDTO { Title = "Inception", ReleaseYear = 2010, StudioId = 1 };
            //await movieService.AddMovieAsync(movieDTO);
            //await movieDbContext.SaveChangesAsync();

            //ფილმების გამოტანა
            //var movies = await movieService.GetAllMoviesAsync();
            //foreach (var movie in movies)
            //{
            //    Console.WriteLine(movie.ToString());
            //}
        }
    }
}
