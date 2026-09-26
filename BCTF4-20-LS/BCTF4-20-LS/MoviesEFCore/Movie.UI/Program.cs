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
using Microsoft.EntityFrameworkCore;

namespace Movie.UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            //appjson use;
            //IConfiguration configuration = new ConfigurationBuilder()
            //    .SetBasePath(AppContext.BaseDirectory)
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .Build();

            //var _connectionString = configuration.GetConnectionString("DefaultConnection");
            #region without DI container
            MovieDbContext movieDbContext = new MovieDbContext();
            //IMovieRepository movieRepository = new MovieRepository(movieDbContext);
            //IMovieService movieService = new MovieService(movieRepository);
            #endregion


            ////DI container
            var services = new ServiceCollection();

            services.AddDbContext<MovieDbContext>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();

            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IActorService, ActorService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var serviceProvider = services.BuildServiceProvider();

            var movieService = serviceProvider.GetRequiredService<IMovieService>();
            var actorService = serviceProvider.GetRequiredService<IActorService>();

            var studio = new Studio { Name = "Pixar Animation Studios", CountryId = 1 };
            

            ////movie by id:
            var movieById = await movieService.GetMovieById(2);
            Console.WriteLine(movieById.Title);
            movieDbContext.Add(studio);
            await movieDbContext.SaveChangesAsync();

            var st = await movieDbContext.Studios
                .FirstOrDefaultAsync(s => s.Name == "Pixar Animation Studios");
            
            var movieDTO = new CreateMovieDTO { Title = "Home Alone 3", ReleaseYear = 1996, StudioId = st.Id };
            var movieDto2 = new CreateMovieDTO { Title = "Home Alone 4", ReleaseYear = 2004, StudioId = st.Id};

            
            await movieService.AddMovieAsync(movieDTO);
            await movieService.AddMovieAsync(movieDto2);

            var actorDTO = new CreateActorDTO{FirstName = "Macaulay", LastName = "Culkin"};


            await actorService.AddActorAsync(actorDTO);
            Console.WriteLine("AddActorAsync completed");
            await actorService.AddActorAsync(actorDTO);

            var film1 = await movieDbContext.Movies
                .FirstAsync(m => m.Title == "Home Alone 3");

            var film2 = await movieDbContext.Movies
                .FirstAsync(m => m.Title == "Home Alone 4");

            var updateActorMovieDTO = new UpdateActorMovieDTO
            {
                MovieIds = new List<int> { film1.Id, film2.Id }

            };

            await actorService.UpdateActorMovie(1, updateActorMovieDTO);

            var actorsWithMovies = await movieDbContext.Actors
                .Include(a => a.Movies)
                .ToListAsync();

            foreach(var item in actorsWithMovies)
            {
                Console.Write($"{item.FirstName} {item.LastName}");
                foreach (var movie in item.Movies)
                {
                    Console.Write($" - {movie.Title}");
                }
                Console.WriteLine();
            }
            #region old Code
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
            #endregion
        }
    }
}
