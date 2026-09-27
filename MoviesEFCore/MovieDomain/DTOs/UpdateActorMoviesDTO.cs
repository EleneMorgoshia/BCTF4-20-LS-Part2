using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.DTOs
{
    public class UpdateActorMoviesDTO
    {
        //მსახიოების ფილმების დააბდეითება
        public ICollection<int> MovieIds { get; set; }

    }
}
