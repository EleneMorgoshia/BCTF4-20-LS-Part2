using Microsoft.AspNetCore.Mvc;
using Movie.Service.Interfaces;

namespace Movie.Web1.ViewComponents
{
    public class LatestMoviesViewComponent:ViewComponent
    {
        private readonly IMovieService _movieService;
        public LatestMoviesViewComponent(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //კომპონენტის ჩასატრვითი მეთოდი
        public async Task<IViewComponentResult> InvokeAsync(int count = 2)
        {
            var movies = await _movieService.GetAllMoviesAsync();
            var laestMovies = movies.OrderByDescending(m => m.Id)
                .Take(count)
                .ToList();
            return View(laestMovies);
        }
    }
}
