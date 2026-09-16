using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public ICollection<Movie> Movies { get; set; }// navigation property for movie
    }
}

//reflection-ს იყენებს როცა ბევრი ატრიბუტი მაქვს
//ამიტო ჯობია რომ ბევრი არ მქონდეს 