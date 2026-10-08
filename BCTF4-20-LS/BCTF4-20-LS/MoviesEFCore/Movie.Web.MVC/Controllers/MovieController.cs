using Microsoft.AspNetCore.Mvc;
using Movie.Service.Interfaces;

namespace Movie.Web.MVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        { 
            var movies = await _movieService.GetAllMoviesAsync();
            return View(movies);
        }
    }
}
