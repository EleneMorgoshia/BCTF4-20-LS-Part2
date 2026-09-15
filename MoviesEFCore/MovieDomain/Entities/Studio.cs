using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Entities
{
    public class Studio
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }


        //ბაზებში იქნებოდა
        //CONSTRAINTS FK_STUDIO_COUNTRY FOREIGN KEY (CountryId) REFERECES COUNTRY (id)
        //navigation property - ორ entity-ს შორის კავშირი
        public int CountryId { get; set; } // ეს არის foreign key, ანუ ფროფერთი, რომელშიც შეინახება დაკავშრებული ცხრილის/კლასის(Country-ს) Id
        public Country Country { get; set; } // Navigation Property -  Studio-ს აკავშირებს Country - თან
        //თვითონ ხვდება რომ Country-ს primary key არის Id, ამიტომ გვაძლევს წვდომას იმ ფროფერთიზე.
   
        
        public StudioDetails StudioDetails { get; set; } //navigation property for studioDetails
    
        public ICollection<Movie> Movies { get; set; } // navigation property for movies
    } 
}

