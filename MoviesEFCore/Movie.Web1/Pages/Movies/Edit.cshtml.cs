using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movie.Domain.DTOs;
using Movie.Service.Interfaces;

namespace Movie.Web1.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly IMovieService _movieService;
        public EditModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [BindProperty]
        public UpdateMovieDTO Movie { get; set; }

        [BindProperty]
        public int Id { get; set; }
        public void OnGet(int id)
        {
            Id = id;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _movieService.UpdateMovieAsync(Id, Movie);
                TempData["Success"] = "Movie updated successfully";

                //will be sent to movie page
                return RedirectToPage("/Movies/Index");
            }
            catch (ArgumentException ex) 
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}
