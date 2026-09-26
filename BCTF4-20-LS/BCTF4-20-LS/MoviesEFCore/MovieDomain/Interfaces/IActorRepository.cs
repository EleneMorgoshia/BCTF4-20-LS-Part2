using Movie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Interfaces
{
    public interface IActorRepository
    {
        Task AddActorAsync(Actor actor);
        Task<ICollection<Actor>> GetAllActorsAsync();
        Task<Actor?> GetActorById(int id);
        Task UpdateAcotrAsync(int id, Actor actor);
        Task DeleteActorAsync(int id);
        Task UpdateActorMovieAsync(int actorId, ICollection<int> movieIds);
    
    }
}
