using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movie.Domain.DTOs;
using Movie.Service.Interfaces;

namespace Movie.Web1.Pages.Movies
{
    public class CreateModel : PageModel
    {
        //access with movie service
        private readonly IMovieService _movieService;

        public CreateModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public void OnGet()
        {
        }


        // created property for the movie creation
        [BindProperty]
        public CreateMovieDTO Movie { get; set; }


        public async Task OnPostAsync()
        {
            try
            {
                //CALL THE service method - addMovieAsync
                await _movieService.AddMovieAsync(Movie);
                TempData["Success"] = "Movie Created Successfully";
                Movie = new();
                ModelState.Clear();
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

        }
    }
}
