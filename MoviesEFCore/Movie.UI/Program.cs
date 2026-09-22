using Microsoft.Extensions.Configuration;
using Movie.Domain.DTOs;
using Movie.Domain.Entities;
using Movie.Domain.Interfaces;
using Movie.Infrastructure.Data;
using Movie.Infrastructure.Repositories;
using Movie.Service.Implementations;
using Movie.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            var context = new MovieDbContext(_connectionString);
            
            #region without DI container
            MovieDbContext movieDbContext = new MovieDbContext(_connectionString);
            IMovieRepository movieRepository = new MovieRepository(movieDbContext);
            IMovieService movieService = new MovieService(movieRepository);
            #endregion


            ////DI container
            //var services = new ServiceCollection();

            //services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(_connectionString));
            //services.AddScoped<IMovieRepository, MovieRepository>();// addScoped ით ვამათბ სერვისებს
            //services.AddScoped<IMovieService, MovieService>();
            //var serviceProcider = services.BuildServiceProvider();

            //var movieService = serviceProcider.GetRequiredService<IMovieService>();

            var movieById = await movieService.GetMovieById(1);
            Console.WriteLine(movieById.Title);

            //udpate movie
            //var updatedMovie = new UpdateMovieDTO
            //{
            //    Id = 1,
            //    Title = "The matrix Updated",
            //    ReleaseYear = 1999,
            //    StudioId = 1
            //};
            //await movieService.UpdateMovieAsync(updatedMovie);

            //delete movie
            await movieService.DeleteMovieAsync(movieById.Id);
        }
    }
}
