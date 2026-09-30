using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movie.Domain.DTOs;
using Movie.Service.Interfaces;

namespace Movie.Web1.Pages.Movies
{
    public class IndexModel : PageModel
    {

        private readonly IMovieService _movieSerice;
        
        //???? ???????? ????????, ???????? ???????? ????????? ??????? ?????????? ???????
        public ICollection<MovieDTO> Movies { get; set; } = new List<MovieDTO>();

        public IndexModel(IMovieService movieSerice)
        {
            _movieSerice = movieSerice;
        }

       
        public async Task OnGetAsync()
        {
            Movies = await _movieSerice.GetAllMoviesAsync();

        }


        
        //public List<string> Movies = new List<string>();
        //public void OnGet()
        //{
        //    Movies.Add("The Matrix");
        //    Movies.Add("The Matrxi Reloaded");
        //    Movies.Add("The Matrix Revolutions");
        //}


    }
}
