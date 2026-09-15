using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Movie.Domain.Entities;
using Movie.Infrastructure.Data;


namespace Movie.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var _connectionString = configuration.GetConnectionString("DefaultConnection");
            var context = new MovieDbContext(_connectionString); // ობიექტი დატა ფოლდერის MovieDbContext-ის კლასის
            //context.Database.EnsureCreated(); //ამით კეთდება მიგრაციები. ეს შექმნის ცხრილებს და გააკეთბს კავშირებს


            Country country3 = new Country();
            country3.Name = "Italy";
            //country and studio relations
            country3.Studios = new List<Studio>();


            Studio studio1 = new Studio();
            studio1.Name = "Rainbow";
            //studio and country relation
            studio1.Country = country3;
            //studion and movie relation
            studio1.Movies = new List<Movie.Domain.Entities.Movie>();


            StudioDetails studioDetails1 = new StudioDetails();
            studioDetails1.LicenseNumber = "889945456468";
            //studio and stuiod details relation
            studioDetails1.Studio = studio1;
            //studioDetails and studio relation
            studio1.StudioDetails = studioDetails1;

            Movie.Domain.Entities.Movie movie1 = new Movie.Domain.Entities.Movie();
            movie1.Title = "The Art of Happiness";
            movie1.ReleaseYear = 2013;
            movie1.Studio = studio1;
            //movie and actors relation
            movie1.Actors = new List<Actor>();

            Actor actor1 = new Actor();
            actor1.FirstName = "Luca";
            actor1.LastName = "Testa";
            //actors and movies lreaitons
            actor1.Movies = new List<Movie.Domain.Entities.Movie>();


            country3.Studios.Add(studio1);
            studio1.Movies.Add(movie1);
            movie1.Actors.Add(actor1);
            actor1.Movies.Add(movie1);

            context.Countries.Add(country3);
            context.SaveChanges();

        }
    }
}
