using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movie.Domain.DTOs;
using Movie.Service.Interfaces;

namespace Movie.Web1.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly IMovieService _movieSerice;

        public DetailsModel(IMovieService movieSerice)
        {
            _movieSerice = movieSerice;
        }

        public MovieDTO Movie { get; set; }

        public async Task OnGet(int id)
        {
            Movie = await _movieSerice.GetMovieById(id);

        }
    }
}
