using Movie.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Interfaces
{
    public interface IActorService
    {
        Task<ICollection<ActorDTO>> GetAllActorsAsync();
        Task<ActorDTO?> GetActorById(int id);
        Task AddActorAsync(CreateActorDTO actor);

        Task UpdateAcotrAsync(int id, UpdateActorDTO actor);

        Task DeleteActorAsync(int id);

        Task UpdateActorMovies(int actorId, UpdateActorMoviesDTO UpdateActorMoviesDTO);
    }
}
