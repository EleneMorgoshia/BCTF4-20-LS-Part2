using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; } //id -  თვითონ ხვდება რომ არის priary key . არ გვჭირდება [key].

        [MaxLength(100)]
        public string Name { get; set; }
        

        //ანუ ერთ ქვეყანაში შეიძლება იყოს ბევრი სტუდიო
        public ICollection<Studio> Studios { get; set; } // navigation property
    }
}
