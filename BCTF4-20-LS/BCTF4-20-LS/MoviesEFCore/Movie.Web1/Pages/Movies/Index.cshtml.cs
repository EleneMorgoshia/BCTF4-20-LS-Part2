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


        [BindProperty]
        public int Year {  get; set; }

        [BindProperty]
        public string StudioName { get; set; }

        [BindProperty]
        public int MinimumActorCount { get; set; }

 

        //year , studioName ,minimumActorCount       
        public async Task OnGetAsync()
        {
            Movies = await _movieSerice.GetAllMoviesAsync();

        }

        //public async Task OnPostDeleteAsync(int id)
        //{
        //    try
        //    {
        //        await _movieSerice.DeleteMovieAsync(id);
        //        TempData["Success"] = "Movie Deleted successfully";
        //    }
        //    catch (Exception ex) 
        //    {
        //        TempData["Error"] = ex.Message;
        //    }

        //    //to get new movie collection(without deleted movie)
        //    await OnGetAsync();
        //}

        //IActionResult-  returns a response action.
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                await _movieSerice.DeleteMovieAsync(id);
                TempData["Success"] = "Movie Deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            //to get new movie collection(without deleted movie)
            return RedirectToPage();
        }

        public async Task OnPostSearchAsync()
        {
            try
            {
                var searchedResult = await _movieSerice.GetMoviesByStudioAsync(Year, StudioName, MinimumActorCount);
                Movies = searchedResult.Select(m => new MovieDTO
                {
                    Title = m.Title,
                    StudioName = m.StudioName,
                    ReleaseYear = m.ReleaseYear
                }).ToList();
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
            }

        }

        public async Task OnPostResetAsync()
        {
            try
            {
                await OnGetAsync();

            }
            catch (ArgumentException ex) 
            {
                TempData["Error"] = ex.Message;
            }
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
