using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        //foreign key for studio + navigation proeprty
        public int StudioId { get; set; } // foreign key 
        public Studio Studio { get; set; } // navigation property for studio

        public ICollection<Actor> Actors { get; set; } //navigation property for actors table
    }
}
