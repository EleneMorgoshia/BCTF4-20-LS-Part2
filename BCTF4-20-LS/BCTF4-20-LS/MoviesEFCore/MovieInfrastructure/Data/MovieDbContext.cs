using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Infrastructure.Data
{
    public class MovieDbContext : DbContext
    {
        private readonly string _connectionString;

        //CONNECTION სტრინგი რო გადმოვცე di-დან
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        //როცა გავუშვებ ასეთი ცხრილები შემქმნება
        //dbset ეუბნება რომ ცხრილი შექმენი
        public DbSet<Country> Countries { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<StudioDetails> StudiosDetails { get;set;}
        public DbSet<Movie.Domain.Entities.Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }


        // Database-თან კავშირის კონფიგურაცია - აქ ვუთითებთ connection String-ს
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);//აქ ჩემი ქონექშენ სტრინგი
        }

        //Entities-ის და მათ შორის რელაციებს, ყველაფერს ეს აკეთებს 
        //junction table-საც ეს ქმნის აქ შემიძლია სახელი შევუცვალო დეფოლთად თვითნ არქმევს ცხრილების მიხედვით
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //country vs studio - (one - to - many)
            //studio vs movie - (one - to - many)
            //studio vs studio details - (one - to - one)
            //movie vs actor -  (many - to - many)
            base.OnModelCreating(modelBuilder);

            //country vs studio - (one - to - many)
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Studios)
                .WithOne(s => s.Country)
                .HasForeignKey(s => s.CountryId);

            //studio vs movie - (one - to - many)
            //1 studio - many movie
            //movie - 1 studio
            modelBuilder.Entity<Studio>()
                .HasMany(s => s.Movies)
                .WithOne(m => m.Studio)
                .HasForeignKey(m => m.StudioId);

            //studio vs studio details -(one - to - one)
            modelBuilder.Entity<Studio>()
                .HasOne(s => s.StudioDetails)
                .WithOne(st => st.Studio)
                .HasForeignKey<StudioDetails>(st => st.StudioId);

            //movie vs actor -  (many - to - many)
            modelBuilder.Entity<Movie.Domain.Entities.Movie>()
                .HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .UsingEntity(ma => ma.ToTable("MoviesActors")); //ამით ვეუბნები რომ junction ცხრილს დაარქვას MOVIESACTORS 


            //data seeding - defaultად ვარდეაბა ეს დატა ბაზაში
            modelBuilder.Entity<Country>()
                .HasData(
                    new Country { Id = 1, Name = "USA" },
                    new Country { Id = 2, Name = "UK" },
                    new Country { Id = 3, Name = "France" }

                );
        }
    }
}
