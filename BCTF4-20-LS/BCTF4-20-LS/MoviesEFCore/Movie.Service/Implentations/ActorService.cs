using Movie.Domain.DTOs;
using Movie.Domain.Entities;
using Movie.Domain.Interfaces;
using Movie.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Implentations
{
    public class ActorService : IActorService
    {

        private readonly IActorRepository _actorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActorService(IActorRepository actorRepository, IUnitOfWork unitOfWork)
        {
            _actorRepository = actorRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<ActorDTO>> GetAllActorsAsync()
        {
           var actors = await _actorRepository.GetAllActorsAsync();
           var actorDtos = actors.Select(a => new ActorDTO
           {
               FirstName = a.FirstName,
               LastName = a.LastName,

               MoviesTitles = a.Movies.Select(m => m.Title).ToList()
           }).ToList();
            
            return actorDtos;
        }
        public async Task<ActorDTO?> GetActorById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("id cannot be negative or 0");
            var actorById = await _actorRepository.GetActorById(id);
            if (actorById == null)
                throw new ArgumentException("id cannot be negative or 0");

            var actorDTO = new ActorDTO
            {
                FirstName = actorById.FirstName,
                LastName = actorById.LastName
            };

            return actorDTO;
        }
        public async Task AddActorAsync(CreateActorDTO actor)
        {
            if(actor == null)
                throw new ArgumentNullException(nameof(actor));

            var actorEntity = new Actor
            {
                FirstName = actor.FirstName,
                LastName = actor.LastName
            };
            
            await _actorRepository.AddActorAsync(actorEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAcotrAsync(int id, UpdateActorDTO actor)
        {
            if (id <= 0)
                throw new ArgumentException("id cannot be negative or 0");
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            var actorEntity = new Actor
            {

                FirstName = actor.FirstName,
                LastName = actor.LastName
            };
            await _actorRepository.UpdateAcotrAsync(id, actorEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteActorAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("id cannot be negative or 0");

            _actorRepository.DeleteActorAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        
        
        public async Task UpdateActorMovie(int actorId, UpdateActorMovieDTO UpdateActorMoviesDTO)
        {
            if(actorId <= 0)
                throw new ArgumentException("id cannot be negative or 0");

            if(UpdateActorMoviesDTO == null)
                throw new ArgumentNullException(nameof(UpdateActorMoviesDTO));

            _actorRepository.UpdateActorMovieAsync(actorId, UpdateActorMoviesDTO.MovieIds);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
