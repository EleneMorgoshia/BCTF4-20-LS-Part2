using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities;
using Movie.Domain.Interfaces;
using Movie.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Infrastructure.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly MovieDbContext _movieContext;
        private readonly IUnitOfWork _unitOfWork;
        public ActorRepository(MovieDbContext movieContext, IUnitOfWork unitOfWork)
        {
            _movieContext = movieContext;
            _unitOfWork = unitOfWork;
        }

        public async Task AddActorAsync(Actor actor)
        {
            await _movieContext.Actors.AddAsync(actor);
        }

        //ეს აბრუნებს კოლექციას
        public async Task<ICollection<Actor>> GetAllActorsAsync()
        {
            return await _movieContext.Actors
                .Include(a => a.Movies)
                .ToListAsync();
        }

        public async Task<Actor?> GetActorById(int id)
        {
            return await _movieContext.Actors
                .Include(a => a.Movies)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAcotrAsync(int id, Actor actor)
        {
            var actorById = await _movieContext.Actors.FirstOrDefaultAsync(a => a.Id == id);
            if (actorById == null)
                throw new ArgumentException("There is no actor with this id");

            _movieContext.Actors.Update(actorById);
        }

        public async Task DeleteActorAsync(int id)
        {
            var actorById = await _movieContext.Actors.FirstOrDefaultAsync(a => a.Id == id);
            if (actorById == null)
                throw new ArgumentException("there is no actor with this id");
            _movieContext.Actors.Remove(actorById);
        }

        public async Task UpdateActorMovieAsync(int actorId, ICollection<int> movieIds)
        {
            //დავააბდეითოთ მსახიობის ფილმები
            var actorById = await _movieContext.Actors.FirstOrDefaultAsync(a => a.Id == actorId);
            if (actorById == null)
                throw new ArgumentException("There is no actor with this id");

            //ისეთი ფილმები წამოვიღოთ, რომელიც არის ამ კონკრეტული აქტორის(ანუ ეს მსახიობი რომელშიც თამაშობს მარტო ეგენი)
            var movies = await _movieContext.Movies
                .Where(m => movieIds.Contains(m.Id))
                .ToListAsync();

            //ყველა ფილმს ვშლით  ამ აქტორისსას
            actorById.Movies.Clear();

            //ყველა ფილმს ვამატებთ თავიდან
            foreach (var movie in movies)
            {
                actorById.Movies.Add(movie);
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
