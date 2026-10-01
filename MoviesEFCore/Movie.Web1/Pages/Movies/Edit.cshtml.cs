using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movie.Service.Interfaces;

namespace Movie.Web1.Pages.Movies
{
    public class AddModel : PageModel
    {
        private readonly IMovieService _movieService;

        public AddModel(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public int ReleaseYear { get; set; }
        [BindProperty]
        public int StudioId { get; set; }
        public void OnGet()
        {
        }
    }
}
