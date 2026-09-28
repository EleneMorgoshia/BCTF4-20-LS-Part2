using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.DTOs
{
    public class SearchMovieDTO
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string StudioName { get; set; }
        public string CountryName { get; set; }
        public int ActorsCount { get; set; }

        public override string ToString()
        {
            return $"{Title}, {ReleaseYear}, {StudioName}, {CountryName}, {ActorsCount}";
        }
    }
}
